using Jpinsoft.Class2GUI.GUIGenerators.WPF.GControls;
using Jpinsoft.Class2GUI.Properties;
using Jpinsoft.Class2GUI.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jpinsoft.Class2GUI.GUIGenerators.WPF
{
    public class WPFClass2GUIBuilder
    {
        #region Fields

        public Type TargetType { get; private set; }

        public GRenderOutput RenderOutput { get; private set; }

        public string OutNamespace { get; private set; }

        public byte MaxRecursuinLevel { get; private set; }

        public List<WPFClass2GUIBuilder> InnerBuilders { get; private set; }

        public List<PropertyInfo> TargetProperties { get; private set; }

        /// <summary>
        /// Unknown Types which we need generate
        /// </summary>
        public List<Type> UnknownTypes { get; private set; }

        #endregion

        #region Public

        public void BuildControls(Type targetType, List<PropertyInfo> targetProperties, string outNamespace, byte maxRecursionLevel)
        {
            Init(targetType, targetProperties, outNamespace, maxRecursionLevel);

            InnerBuilders = new List<WPFClass2GUIBuilder>();
            InnerBuilders.Add(this);

            // Recursion
            BuildControlsRecursive(targetType, targetProperties, outNamespace, maxRecursionLevel, 1, InnerBuilders);

            List<IGrouping<Type, WPFClass2GUIBuilder>> duplicities = InnerBuilders.GroupBy(r => r.TargetType).Where(g => g.Count() > 1).ToList();

            if (duplicities.Count() > 0)
            {
                Console.WriteLine("Vysledok obsahuje duplicitne typy.");
            }
            // ----------------------------
        }

        public void Render()
        {
            // Dialog buttons
            RenderOutput.Controls.Add(new DialogButtonsGenControl());

            foreach (GControl control in RenderOutput.Controls)
            {
                control.Render(RenderOutput);

                RenderOutput.Xaml.AppendLine();
                RenderOutput.ViewModel.AppendLine();
            }

            // Insert Xaml to parrent Container
            RenderOutput.Xaml = new StringBuilder().AppendFormat(Resources_WPFGenerator.T003, OutNamespace, TargetType.Namespace, TargetType.Name, RenderOutput.GridRows, RenderOutput.Xaml, RenderOutput.XamlResources, RenderOutput.XamlWindowAttributes);

            // Insert CodeBehind to parrent Class
            RenderOutput.XamlCodebehind = new StringBuilder().AppendFormat(Resources_WPFGenerator.T013, OutNamespace, TargetType.Namespace, TargetType.Name, RenderOutput.XamlCodebehind);

            // Insert ViewModel to parrentClass
            RenderOutput.ViewModel = new StringBuilder().AppendFormat(Resources_WPFGenerator.T014, OutNamespace, RenderOutput.CsUsingReferences, TargetType.Name, RenderOutput.ViewModel);

            // Generate ViewModel base class
            RenderOutput.ViewModelBase = new StringBuilder().AppendFormat(Resources_WPFGenerator.T015, OutNamespace);

            // DisplayName converter helper
            RenderOutput.DisplayNameConverter = RenderOutput.DisplayNameConverter.AppendFormat(Resources_WPFGenerator.T016, OutNamespace);
        }

        #endregion

        #region Private

        private void Init(Type targetType, List<PropertyInfo> targetProperties, string outNamespace, byte maxRecursionLevel)
        {
            // INIT
            RenderOutput = new GRenderOutput();
            this.OutNamespace = outNamespace;

            IncludeNameSpace(outNamespace);
            this.TargetType = targetType;
            this.TargetProperties = targetProperties;

            UnknownTypes = new List<Type>();
            MaxRecursuinLevel = maxRecursionLevel;
            // ---------------
        }


        // TODo Problem ak je DLL z novsieho .NET ako je generovana WPFOut apka
        // TODO generovat iba objekty zo SYSTEM, alebo z generovanej DLL
        // TODo prepinac povolujuci aj typy, ktore su z inych Assemblies
        private void BuildControlsRecursive(Type targetType, List<PropertyInfo> targetProperties, string outNamespace, byte maxRecursionLevel, int recursionLevel, List<WPFClass2GUIBuilder> generators)
        {
            Init(targetType, targetProperties, outNamespace, maxRecursionLevel);

            this.InnerBuilders = generators;

            foreach (PropertyInfo propInfo in TargetProperties)
            {
                if (propInfo.PropertyType.IsEnum)
                {
                    IncludeNameSpace(propInfo.PropertyType.Namespace, propInfo.PropertyType.Assembly.GetName().Name);
                    RenderOutput.Controls.Add(new EnumComboBoxGenControl { BindedProperty = propInfo });
                    continue;
                }

                switch (System.Type.GetTypeCode(propInfo.PropertyType))
                {
                    case TypeCode.Boolean:
                        RenderOutput.Controls.Add(new CheckBoxGenControl { BindedProperty = propInfo });
                        break;
                    case TypeCode.Byte:
                        RenderOutput.Controls.Add(new NumGenControl { BindedProperty = propInfo, Minimum = byte.MinValue, Maximum = byte.MaxValue });
                        break;
                    case TypeCode.Char:
                        RenderOutput.Controls.Add(new NumGenControl { BindedProperty = propInfo, GenerateAsSlider = false });
                        break;
                    case TypeCode.DBNull:
                        break;
                    case TypeCode.DateTime:
                        RenderOutput.Controls.Add(new DateTimeGenControl { BindedProperty = propInfo });
                        break;
                    case TypeCode.Decimal:
                        RenderOutput.Controls.Add(new NumGenControl { BindedProperty = propInfo, GenerateAsSlider = false });
                        break;
                    case TypeCode.Double:
                        RenderOutput.Controls.Add(new NumGenControl { BindedProperty = propInfo, GenerateAsSlider = false });
                        break;
                    case TypeCode.Empty:
                        break;
                    case TypeCode.Int16:
                        RenderOutput.Controls.Add(new NumGenControl { BindedProperty = propInfo });
                        break;
                    case TypeCode.Int32:
                        RenderOutput.Controls.Add(new NumGenControl { BindedProperty = propInfo });
                        break;
                    case TypeCode.Int64:
                        RenderOutput.Controls.Add(new NumGenControl { BindedProperty = propInfo });
                        break;
                    case TypeCode.Object:

                        // Pri Object typoch je potrebne mysliet aj na dalsiu uroven, aby sa pre tento ty vygenerovala
                        if (recursionLevel + 1 >= MaxRecursuinLevel)
                            break;

                        // IEnumerable/Listy rieseny ako listview

                        Type genericArgument = ClassToGUITools.GetEnumerableType(propInfo.PropertyType);

                        if (genericArgument != null)
                        {
                            if (genericArgument.Assembly != targetType.Assembly)
                                break;

                            UnknownTypes.Add(genericArgument);

                            RenderOutput.Controls.Add(new DataGridGenControl { BindedProperty = propInfo });
                            break;
                        }

                        if (propInfo.PropertyType.Assembly != targetType.Assembly)
                            break;

                        UnknownTypes.Add(propInfo.PropertyType);

                        // Single Object rieseny ako LookUp cez ComboBox
                        // TODO: Option - detail vnoreneho objektu
                        // RenderContext.Controls.Add(new ComboBoxGenControl { BindedProperty = propInfo });
                        RenderOutput.Controls.Add(new ObjectDetailGenControl { BindedProperty = propInfo });

                        break;

                    case TypeCode.SByte:
                        RenderOutput.Controls.Add(new NumGenControl { BindedProperty = propInfo, Minimum = sbyte.MinValue, Maximum = sbyte.MaxValue });
                        break;
                    case TypeCode.Single:
                        RenderOutput.Controls.Add(new NumGenControl { BindedProperty = propInfo, GenerateAsSlider = false });
                        break;
                    case TypeCode.String:
                        RenderOutput.Controls.Add(new TextBoxGenControl { BindedProperty = propInfo });
                        break;
                    case TypeCode.UInt16:
                        RenderOutput.Controls.Add(new NumGenControl { BindedProperty = propInfo, Minimum = 0 });
                        break;
                    case TypeCode.UInt32:
                        RenderOutput.Controls.Add(new NumGenControl { BindedProperty = propInfo, Minimum = 0 });
                        break;
                    case TypeCode.UInt64:
                        RenderOutput.Controls.Add(new NumGenControl { BindedProperty = propInfo, Minimum = 0 });
                        break;
                    default:
                        break;
                }
            }

            if (recursionLevel == MaxRecursuinLevel)
                return;

            foreach (Type unknownType in this.UnknownTypes)
            {
                if (generators.Count(g => g.TargetType == unknownType) > 0)
                    continue;

                generators.Add(new WPFClass2GUIBuilder());
                generators.Last().BuildControlsRecursive(unknownType, unknownType.GetProperties().ToList(), this.OutNamespace, this.MaxRecursuinLevel, recursionLevel + 1, generators);
            }
        }

        /// <summary>
        /// IncludeNameSpace into XAML, XAMLCODEBEHIND, VIEWMODEL
        /// </summary>
        private void IncludeNameSpace(string nameSpace, string assmebly = null)
        {
            // System is included in Templates, so we can skip this namespace
            if (nameSpace == "System")
                return;

            string nsfullKey;

            if (assmebly == null || assmebly == Assembly.GetExecutingAssembly().GetName().Name)
            {
                assmebly = string.Empty;

                nsfullKey = nameSpace + "_" + Assembly.GetExecutingAssembly().GetName().Name;
            }
            else
                nsfullKey = nameSpace + "_" + assmebly;

            if (!RenderOutput.NamespacePrefixTable.ContainsKey(nsfullKey))
            {
                // Jedinecny kluc musi byt namespace + assembly name, pretoze rovnaky namespace moze byt vo viacerych assemblies
                RenderOutput.NamespacePrefixTable.Add(nsfullKey, new NamespaceInfo { Namespace = nameSpace, Prefix = "ns" + string.Format("{0:d2}", RenderOutput.NamespacePrefixTable.Count + 1), AssemblyName = assmebly });

                RenderOutput.XamlWindowAttributes.AppendFormat(Resources_WPFGenerator.T026, RenderOutput.NamespacePrefixTable[nsfullKey].Prefix, nameSpace, assmebly);
                RenderOutput.XamlWindowAttributes.AppendLine();

                // Aby rovnaky namespace nebol 2x v kode, pretoze pri using sa nehladi na assembly
                if (RenderOutput.NamespacePrefixTable.Where(item => item.Value.Namespace == nameSpace).Count() < 2)
                {
                    RenderOutput.CsUsingReferences.AppendFormat(Resources_WPFGenerator.T027, nameSpace);
                    RenderOutput.CsUsingReferences.AppendLine();
                }
            }
        }

        #endregion
    }
}
