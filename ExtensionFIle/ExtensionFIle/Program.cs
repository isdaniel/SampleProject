using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExtensionBase;

namespace ExtensionFIle
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filePath = Directory.GetFiles(@"C:\\ExtenFile", "*.dll");
            var assemblies = filePath.Select(Assembly.LoadFile);
            var persons= assemblies.SelectMany(x => x.GetTypes().Where(t=> typeof(IPerson).IsAssignableFrom(t)), (x, t) => Activator.CreateInstance(t) as IPerson);

            foreach (var p in persons)
            {
                Console.WriteLine(p.SayHello());
            }

            Console.ReadKey();
        }
    }
}
