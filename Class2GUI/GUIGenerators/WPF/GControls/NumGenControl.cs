using Jpinsoft.Class2GUI.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jpinsoft.Class2GUI.GUIGenerators.WPF.GControls
{
    public class NumGenControl : GControl
    {
        public NumGenControl()
        {
            Minimum = -1000;
            Maximum = 1000;
            GenerateAsSlider = true;
        }

        public int Minimum { get; set; }

        public int Maximum { get; set; }

        public bool GenerateAsSlider { get; set; }

        private void AddXaml(GRenderOutput renderContext)
        {
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T002, BindedProperty.Name, renderContext.rowNum, renderContext.colNum, BindedProperty.Name);
            renderContext.Xaml.AppendLine();
            renderContext.Xaml.AppendLine();

            if (GenerateAsSlider)
                renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T019, BindedProperty.Name, renderContext.rowNum, renderContext.colNum + 1, renderContext.tabIndex, Minimum, Maximum);
            else
                renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T033, BindedProperty.Name, renderContext.rowNum, renderContext.colNum + 1, renderContext.tabIndex);



            renderContext.tabIndex += 10;
            renderContext.rowNum++;
        }

        private void AddViewModel(GRenderOutput renderContext)
        {
            string fieldName = char.ToString(char.ToLower(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);
            string propName = char.ToString(char.ToUpper(BindedProperty.Name[0])) + BindedProperty.Name.Substring(1, BindedProperty.Name.Length - 1);

            renderContext.ViewModel.AppendFormat(Resources_WPFGenerator.T020, fieldName, propName, BindedProperty.PropertyType.Name);
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
