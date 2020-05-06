using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection_Attribute
{
    public class CustomerAttribute : Attribute
    {
        public string Name{ get; set; }
    }

    [Customer(Name= "A")]
    public class A
    {
        private int _val;
        private int _val2 { get; set; }
        public A()
        {
            _val = 1000;
            _val2 = 200;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            var f = typeof(A).GetField("_val");
            var f1 = typeof(A).GetField("_val",BindingFlags.NonPublic | 
                BindingFlags.Instance);
            var p1 = typeof(A).GetProperty("_val2",BindingFlags.NonPublic | 
                                               BindingFlags.Instance);
            Console.WriteLine(f1.GetValue(a));

            var attr = typeof(A).GetCustomAttribute(typeof(CustomerAttribute));
            Console.ReadKey();
        }
    }
}
