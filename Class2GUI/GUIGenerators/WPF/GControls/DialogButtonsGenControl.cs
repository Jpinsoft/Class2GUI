using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jpinsoft.Class2GUI.Properties;

namespace Jpinsoft.Class2GUI.GUIGenerators.WPF.GControls
{
    public class DialogButtonsGenControl : GControl
    {
        public override void Render(GRenderOutput renderContext)
        {
            renderContext.Xaml.AppendFormat(Resources_WPFGenerator.T021, renderContext.rowNum, renderContext.colNum + 1);
            renderContext.Xaml.AppendLine();

            renderContext.XamlCodebehind.AppendLine(Resources_WPFGenerator.T022);

        }
    }
}
