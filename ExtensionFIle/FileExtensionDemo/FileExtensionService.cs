using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public IEnumerable<PluginModel<T>> LoadPluginMethod<T>()
            where T:class
        {
            string[] filePath = Directory.GetFiles(_dllPath, "*.dll");
            var assemblies = filePath.Select(Assembly.LoadFile);
            return assemblies.SelectMany(x => x.GetTypes().Where(t=> typeof(T).IsAssignableFrom(t)), (x, t) => 
                new PluginModel<T>()
                {
                    Description = t.GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty,
                    Instance = Activator.CreateInstance(t) as T
                });

        }
    }

    public class PluginModel<T>
        where T:class
    {
         public T Instance { get; set; }
         public string Description { get; set; }
    }
}