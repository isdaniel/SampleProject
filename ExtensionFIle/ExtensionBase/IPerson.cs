using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBase
{
    public interface IPerson
    {
        string SayHello();
    }

    public interface IDiscount
    {
        decimal GetPrice(IEnumerable<DiscountDto> discounts);
    }

    public class DiscountDescriptionAttribute : Attribute
    {
        public string Name { get; set; }
    }

    public class DiscountDto
    {
        public int Amount{ get; set; }
        public decimal Price { get; set; }
    }
}
