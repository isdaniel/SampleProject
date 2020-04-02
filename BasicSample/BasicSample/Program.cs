using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 *  switch case,
    if else,
    do...while,
    while,
    for....loop

    array
    IEnumerable<>
    foreach

    refer type vs value type
 *
 */
namespace BasicSample
{
    public enum SexStatus
    {
        Man,
        Women
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region for...loop sample
            //(設定初始值;設定條件;每跑一次執行動作)
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine( ($"Hi {i}"));
            //}
            #endregion

            #region while sample
            //int index = 0;
            //while (index < 100)
            //{
            //   Console.WriteLine( ($"Hi {index}"));
            //   index++;
            //}
            #endregion

            #region do...while sample
            //int index = 0;
            //do
            //{
            //    Console.WriteLine( ($"Hi {index}"));
            //    index++;
            //} while (index < 100); 
            #endregion

            #region do...while vs while
            //int index = 0;
            //do
            //{
            //    Console.WriteLine("do while Hello");
            //} while (index > 1);

            //while (index > 1)
            //{
            //    Console.WriteLine("while Hello");
            //}
            #endregion

            #region switch case and if..else
            //switch case 只能判斷常數(較常搭配enum一起使用)
            //if..else可以判斷運算元或函式結果  
            SexStatus sex;
            string input = Console.ReadLine();
            if (Enum.TryParse(input,out sex))
            {
                switch (sex)
                {
                    case SexStatus.Man:
                        Console.WriteLine("Hi Man");
                        break;
                    case SexStatus.Women:
                        Console.WriteLine("Hi Women");
                        break;
                }
            }

            #endregion

            #region array
            int[] arr = new int[] {1,2,3,4,5};
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
            #endregion

            Console.ReadKey();
        }
    }
}
