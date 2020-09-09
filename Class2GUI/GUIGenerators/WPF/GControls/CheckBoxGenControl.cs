using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jpinsoft.Class2GUI.Properties;

namespace Jpinsoft.Class2GUI.GUIGenerators.WPF.GControls
{
    public class CheckBoxGenControl : GControl
    {
        private void AddXaml(GRenderOutput renderContext)
        {
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T002, BindedProperty.Name, renderContext.rowNum, renderContext.colNum, BindedProperty.Name);
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T005, "Chb" + BindedProperty.Name, renderContext.rowNum, renderContext.colNum + 1, renderContext.tabIndex, BindedProperty.Name);

            renderContext.tabIndex += 10;
            renderContext.rowNum++;
        }

        private void AddViewModel(GRenderOutput renderContext)
        {
            string fieldName = char.ToString(char.ToLower(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);
            string propName = char.ToString(char.ToUpper(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);

            renderContext.ViewModel.AppendFormat(Resources_WPFGenerator.T006, fieldName, propName);
            renderContext.ViewModel.AppendLine();
            renderContext.ViewModel.AppendLine();
        }

        public override void Render(GRenderOutput renderContext)
        {
            AddXaml(renderContext);
            AddViewModel(renderContext);

            renderContext.GridRows.AppendLine("<RowDefinition MinHeight=\"30\" Height=\"40\" />");
        }
    }
}
