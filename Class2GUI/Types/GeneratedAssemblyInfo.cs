using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jpinsoft.Class2GUI.Types
{
    public class GeneratedAssemblyInfo
    {
        public string AssemblyFileName { get { return TargetAssembly.GetName().Name + ".dll"; } }

        public Assembly TargetAssembly { get; set; }

        public List<GeneratedTypeInfo> AssemblyTypes { get; set; }

        public byte[] AssemblyRawData { get; set; }
    }
}
