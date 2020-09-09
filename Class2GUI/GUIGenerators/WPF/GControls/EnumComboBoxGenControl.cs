using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Jpinsoft.Class2GUI.Properties;

namespace Jpinsoft.Class2GUI.GUIGenerators.WPF.GControls
{
    public class EnumComboBoxGenControl : GControl
    {
        private void AddXaml(GRenderOutput renderContext)
        {
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T002, BindedProperty.Name, renderContext.rowNum, renderContext.colNum);
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T024, BindedProperty.Name, renderContext.rowNum, renderContext.colNum + 1, renderContext.tabIndex, BindedProperty.PropertyType.Name);
            renderContext.tabIndex += 10;

            renderContext.rowNum++;
        }

        private void AddViewModel(GRenderOutput renderContext)
        {
            string fieldName = char.ToString(char.ToLower(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);
            string propName = char.ToString(char.ToUpper(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);

            renderContext.ViewModel.AppendFormat(Resources_WPFGenerator.T025, fieldName, propName, BindedProperty.PropertyType.Name);

            renderContext.ViewModel.AppendLine();
            renderContext.ViewModel.AppendLine();
        }

        // TODO: prevent to render multiple XAML EnumSource in Resource
        // include Enum namespace to xaml
        public override void Render(GRenderOutput renderContext)
        {
            AddXaml(renderContext);
            AddViewModel(renderContext);

            if (!renderContext.EnumObjectDataProviderTypes.Contains(BindedProperty.PropertyType))
            {
                renderContext.EnumObjectDataProviderTypes.Add(BindedProperty.PropertyType);

                renderContext.XamlResources.AppendFormat(Resources_WPFGenerator.T023, BindedProperty.PropertyType.Name, renderContext.NamespacePrefixTable[BindedProperty.PropertyType.Namespace + "_" + BindedProperty.PropertyType.Assembly.GetName().Name].Prefix);
                renderContext.XamlResources.AppendLine();
            }

            renderContext.GridRows.AppendLine("<RowDefinition MinHeight=\"30\" Height=\"40\" />");
        }
    }
}
