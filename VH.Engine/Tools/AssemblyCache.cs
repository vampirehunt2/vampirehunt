using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace VH.Engine.Tools {

    public class AssemblyCache {

        private static Hashtable assemblies = new Hashtable();

        public static Assembly GetAssembly(string name) {
            Assembly assembly = (Assembly)assemblies[name];
            if (assembly == null) {
                assembly = Assembly.LoadFrom(name);
                assemblies.Add(name, assembly);
            }
            return assembly;
        }

        public static object CreateObject(string typeName, string assemblyName) {
            Assembly assembly = GetAssembly(assemblyName);
            return assembly.CreateInstance(typeName);
        }

    }
}
