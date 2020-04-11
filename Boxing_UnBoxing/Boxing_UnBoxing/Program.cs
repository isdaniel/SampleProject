using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxing_UnBoxing
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 100;

            HasBoxing();
            NoBoxing();
            Console.ReadKey();
        }

        //string.format如果直接將 int 型別變數直接使用而未使用 ToString() 方法先進行格式化為字串時就會發生 Boxing 處理
        /**
         *  因為string.format使用的傳入參數是Object,假如傳入參數是int或者其他Value Type型別就會出現Boxing操作,string.format最後會在呼叫ToString方法將Object轉換成字串
            public static string Format(
	            string format,
	            param object[] args
            )
         */
        private static void HasBoxing()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            string source = "";
            for (int i = 0; i < 10000000; i++)
            {
                source = $"Number{i},{i},{i}";
            }

            stopWatch.Stop();
            TimeSpan span = stopWatch.Elapsed;
            Console.WriteLine($"Boxing Test 耗時 {span.Milliseconds.ToString()}毫秒");
        }

        /**
        *
        * 如果是傳入i.ToString()字串的話,就不會出現Boxing動作
        * 因為ToString()會回傳字串,並不是一個值.
        */
        private static void NoBoxing()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            string source = "";
            for (int i = 0; i < 10000000; i++)
            {
                source = $"Number{i.ToString()},{i.ToString()},{i.ToString()}";
            }

            stopWatch.Stop();
            TimeSpan span = stopWatch.Elapsed;
            Console.WriteLine($"No Boxing Test 耗時 {span.Milliseconds.ToString()}毫秒");
        }
    }
}
