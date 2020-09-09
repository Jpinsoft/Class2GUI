using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jpinsoft.Class2GUI.Types
{
    public interface IGUIGenerator
    {
        string Description { get; }

        void GenerateGUIFiles(Dictionary<string, StringBuilder> outputFiles, Type targetType, string outNamespace, byte maxRecursionLevel);

        void GenerateGUIProject(string outFolder, List<Type> targetTypes, string outNamespace, byte maxRecursionLevel);
    }
}
