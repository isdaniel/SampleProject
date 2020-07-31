using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionBase;

namespace ChineseExtension
{
    public class Chinese:IPerson
    {
        public string SayHello()
        {
            return "你好";
        }
    }
}
