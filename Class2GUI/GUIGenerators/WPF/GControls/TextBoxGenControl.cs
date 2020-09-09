using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jpinsoft.Class2GUI.Properties;

namespace Jpinsoft.Class2GUI.GUIGenerators.WPF.GControls
{
    public class TextBoxGenControl : GControl
    {
        public void AddStringToXaml(GRenderOutput renderContext)
        {
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T002, BindedProperty.Name, renderContext.rowNum, renderContext.colNum);
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T001, BindedProperty.Name, renderContext.rowNum, renderContext.colNum + 1, renderContext.tabIndex);

            renderContext.tabIndex += 10;
            renderContext.rowNum++;
        }

        public void AddStringToViewModel(GRenderOutput renderContext)
        {
            string fieldName = char.ToString(char.ToLower(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);
            string propName = char.ToString(char.ToUpper(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);

            renderContext.ViewModel.AppendFormat(Resources_WPFGenerator.T004, fieldName, propName);
            renderContext.ViewModel.AppendLine();
            renderContext.ViewModel.AppendLine();
        }

        public override void Render(GRenderOutput renderContext)
        {
            AddStringToXaml(renderContext);
            AddStringToViewModel(renderContext);

            renderContext.GridRows.AppendLine("<RowDefinition MinHeight=\"30\" Height=\"40\" />");
        }
    }
}
