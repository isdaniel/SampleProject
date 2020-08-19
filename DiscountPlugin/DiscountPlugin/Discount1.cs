using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ExtensionBase;

namespace DiscountPlugin
{

    [Description("8折扣券")]
    public class Discount1 : IDiscount
    {
        public decimal GetPrice(IEnumerable<DiscountDto> discounts)
        {
            return discounts.Sum(x => x.Amount * x.Price) * 0.8m;
        }
    }

    [Description("數量超過8打5折")]
    public class Discount2 : IDiscount
    {
        public decimal GetPrice(IEnumerable<DiscountDto> discounts)
        {
            var discountPrice = discounts.Where(x => x.Amount > 8).Sum(x => x.Amount * x.Price) / 2;
            return discounts.Sum(x => x.Amount * x.Price) - discountPrice;
        }
    }
}
