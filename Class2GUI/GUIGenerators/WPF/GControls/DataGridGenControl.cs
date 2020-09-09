using Jpinsoft.Class2GUI.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jpinsoft.Class2GUI.GUIGenerators.WPF.GControls
{
    public class DataGridGenControl : GControl
    {
        Type genericArgumentType;

        private void AddXaml(GRenderOutput renderContext)
        {
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T002, BindedProperty.Name, renderContext.rowNum, renderContext.colNum, BindedProperty.Name);
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T010, "DataGrid" + BindedProperty.Name, renderContext.rowNum, renderContext.colNum + 1, renderContext.tabIndex, BindedProperty.Name);

            renderContext.tabIndex += 10;
            renderContext.rowNum++;
        }

        private void AddViewModel(GRenderOutput renderContext)
        {
            // string fieldName = char.ToString(char.ToLower(propInfo.Name[0])) + propInfo.Name.Substring(1, propInfo.Name.Length - 1);
            string propName = char.ToString(char.ToUpper(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);

            renderContext.ViewModel.AppendFormat(Resources_WPFGenerator.T011, genericArgumentType.Name, propName);

            renderContext.ViewModel.AppendLine();
            renderContext.ViewModel.AppendLine();
        }

        private void AddCodeBehind(GRenderOutput renderContext)
        {
            renderContext.XamlCodebehind.AppendFormat(Resources_WPFGenerator.T012, BindedProperty.Name, genericArgumentType.Name);
            renderContext.XamlCodebehind.AppendLine();
        }

        public override void Render(GRenderOutput renderContext)
        {
            genericArgumentType = ClassToGUITools.GetEnumerableType(BindedProperty.PropertyType);

            AddXaml(renderContext);
            AddViewModel(renderContext);
            AddCodeBehind(renderContext);

            renderContext.GridRows.AppendLine("<RowDefinition MinHeight=\"30\" Height=\"160\" />");
        }
    }
}
