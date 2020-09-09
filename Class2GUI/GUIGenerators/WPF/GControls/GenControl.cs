using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jpinsoft.Class2GUI.GUIGenerators.WPF.GControls
{
    public abstract class GControl
    {
        public PropertyInfo BindedProperty { get; set; }

        public abstract void Render(GRenderOutput renderContext);
    }
}
