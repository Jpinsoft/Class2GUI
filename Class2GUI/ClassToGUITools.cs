using Jpinsoft.Class2GUI.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jpinsoft.Class2GUI
{
    public static class ClassToGUITools
    {
        public static void AddIfNotContainsKey<TKey, TValue>(this Dictionary<TKey, TValue> res, TKey key, TValue value)
        {
            if (!res.ContainsKey(key))
                res.Add(key, value);
        }

        public static Type GetEnumerableType(Type type)
        {
            foreach (Type intType in type.GetInterfaces())
            {
                if (intType.IsGenericType
                    && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return intType.GetGenericArguments()[0];
                }
            }
            return null;
        }

        public static GeneratedAssemblyInfo LoadPocoLibrary(string classLibPath)
        {
            return LoadPocoLibrary(File.ReadAllBytes(classLibPath));
        }

        public static GeneratedAssemblyInfo LoadPocoLibrary(byte[] classLibData)
        {
            GeneratedAssemblyInfo aInfo = new GeneratedAssemblyInfo();

            aInfo.AssemblyRawData = classLibData;
            aInfo.TargetAssembly = Assembly.Load(aInfo.AssemblyRawData);
            aInfo.AssemblyTypes = LoadPocoLibrary(aInfo.TargetAssembly);

            return aInfo;
        }

        public static List<GeneratedTypeInfo> LoadPocoLibrary(Assembly classLibrary)
        {
            // Only types WITH some properties
            // Only types FROM loaded assembly
            // Only Non-Generic Types
            return classLibrary.GetTypes().Where(type => type.IsClass && !type.IsGenericType && type.GetProperties().Count() > 0 && (type.BaseType == typeof(object) || type.BaseType.Assembly == classLibrary))
                .Select(t => new GeneratedTypeInfo(t, classLibrary)).ToList();
        }
    }
}
