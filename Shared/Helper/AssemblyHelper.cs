using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helper
{
    public class AssemblyHelper
    {
        public List<Assembly> GetAllAssembly(string dllName)
        {
            List<string> pluginpath = FindPlugin(dllName);
            var list = new List<Assembly>();
            foreach (string filename in pluginpath)
            {
                try
                {
                    string asmname = Path.GetFileNameWithoutExtension(filename);
                    if (asmname != string.Empty)
                    {
                        Assembly asm = Assembly.LoadFrom(filename);
                        list.Add(asm);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return list;
        }
        //查找所有插件的路徑
        private List<string> FindPlugin(string dllName)
        {
            List<string> pluginpath = new List<string>();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string dir = Path.Combine(path, "bin");
            string[] dllList = Directory.GetFiles(dir, dllName);
            if (dllList.Length > 0)
            {
                pluginpath.AddRange(dllList.Select(item => Path.Combine(dir, item.Substring(dir.Length + 1))));
            }
            return pluginpath;
        }

    }
}
