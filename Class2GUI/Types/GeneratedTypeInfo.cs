using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jpinsoft.Class2GUI.Types
{
    public class GeneratedTypeInfo
    {
        public GeneratedTypeInfo(Type type, Assembly assembly)
        {
            this.TypeInfo = type;
            this.Properties = type.GetProperties().Select(t => new GeneratedPropInfo(t)).ToList();
            this.Assembly = assembly;
        }

        public Assembly Assembly { get; set; }

        public string Name { get { return TypeInfo.Name; } }

        public Type TypeInfo { get; set; }

        public List<GeneratedPropInfo> Properties { get; set; }
    }
}
