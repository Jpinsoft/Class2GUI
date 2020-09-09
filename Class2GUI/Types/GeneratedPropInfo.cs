using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jpinsoft.Class2GUI.Types
{
    public class GeneratedPropInfo
    {
        public GeneratedPropInfo(PropertyInfo pocoProperty)
        {
            this.PocoProperty = pocoProperty;
            this.Name = PocoProperty.Name;
        }

        public string Name { get; set; }

        public PropertyInfo PocoProperty { get; set; }
    }
}
