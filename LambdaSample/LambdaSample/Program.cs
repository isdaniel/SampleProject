using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaSample
{

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
 
    class Course
    {
        public string PersonName { get; set; }
        public string Name{ get; set; }
        public int Score{ get; set; }

    }

    class Program
    {
        static void Main(string[] args)
        {
            
 
            List<Person> list = new List<Person>();
            list.Add(new Person(){ Name = "小明", Age = 19});
            list.Add(new Person(){ Name = "小红", Age = 17});
            list.Add(new Person(){ Name = "小强", Age = 20});
 
            List<Course> list2 = new List<Course>();
            list2.Add(new Course(){ PersonName = "小明", Name = "數學", Score = 90});
            list2.Add(new Course(){ PersonName = "小明", Name = "英文", Score = 56});
            list2.Add(new Course(){ PersonName = "小红", Name = "數學", Score = 60});
            list2.Add(new Course(){ PersonName = "小红", Name = "英文", Score = 100});
   
        }
    }
}
