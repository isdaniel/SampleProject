using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FileExtensionDemo
{
    public class FileExtensionService
    {
        private string _dllPath = ConfigurationManager.AppSettings["DLLPath"];

        public IEnumerable<T> LoadPluginMethod<T>()
            where T:class,new()
        {
            string[] filePath = Directory.GetFiles(_dllPath, "*.dll");
            var assemblies = filePath.Select(Assembly.LoadFile);
            return assemblies.SelectMany(x => x.GetTypes().Where(t=> typeof(T).IsAssignableFrom(t)), (x, t) => Activator.CreateInstance(t) as T);

        }
    }
}