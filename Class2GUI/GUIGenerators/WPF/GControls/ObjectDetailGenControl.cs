using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jpinsoft.Class2GUI.Properties;

namespace Jpinsoft.Class2GUI.GUIGenerators.WPF.GControls
{
    public class ObjectDetailGenControl : GControl
    {

        private void AddXaml(GRenderOutput renderContext)
        {
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T002, BindedProperty.Name, renderContext.rowNum, renderContext.colNum);
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T028, BindedProperty.Name, renderContext.rowNum, renderContext.colNum + 1, renderContext.tabIndex);
            renderContext.tabIndex += 10;

            renderContext.rowNum++;
        }

        private void AddViewModel(GRenderOutput renderContext)
        {
            string fieldName = char.ToString(char.ToLower(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);
            string propName = char.ToString(char.ToUpper(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);

            renderContext.ViewModel.AppendFormat(Resources_WPFGenerator.T008, fieldName, propName, BindedProperty.PropertyType.Name);

            renderContext.ViewModel.AppendLine();
            renderContext.ViewModel.AppendLine();
        }

        private void AddCodeBehind(GRenderOutput renderContext)
        {
            renderContext.XamlCodebehind.AppendFormat(Resources_WPFGenerator.T029, BindedProperty.Name, BindedProperty.PropertyType.Name);
            renderContext.XamlCodebehind.AppendLine();
        }

        public override void Render(GRenderOutput renderContext)
        {
            AddXaml(renderContext);
            AddViewModel(renderContext);
            AddCodeBehind(renderContext);

            renderContext.GridRows.AppendLine("<RowDefinition MinHeight=\"30\" Height=\"40\" />");
        }
    }
}
