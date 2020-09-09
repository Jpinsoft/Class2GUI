using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jpinsoft.Class2GUI.Properties;

namespace Jpinsoft.Class2GUI.GUIGenerators.WPF.GControls
{
    public class ComboBoxGenControl : GControl
    {

        private void AddXaml(GRenderOutput renderContext)
        {
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T002, BindedProperty.Name, renderContext.rowNum, renderContext.colNum);
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T007, BindedProperty.Name, renderContext.rowNum, renderContext.colNum + 1, renderContext.tabIndex, BindedProperty.PropertyType.Name);
            renderContext.tabIndex += 10;

            renderContext.rowNum++;
        }

        private void AddViewModel(GRenderOutput renderContext)
        {
            string fieldName = char.ToString(char.ToLower(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);
            string propName = char.ToString(char.ToUpper(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);

            renderContext.ViewModel.AppendFormat(Resources_WPFGenerator.T008, fieldName, propName, BindedProperty.PropertyType.Name);

            if (!renderContext.LookUpSourceInViewModel.Contains(BindedProperty.PropertyType.Name))
            {
                renderContext.ViewModel.Insert(0, string.Format(Resources_WPFGenerator.T009, BindedProperty.PropertyType.Name) + Environment.NewLine + Environment.NewLine);
                renderContext.LookUpSourceInViewModel.Add(BindedProperty.PropertyType.Name);
            }

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
