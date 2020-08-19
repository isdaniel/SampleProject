using System.Collections.Generic;

namespace FileExtensionDemo.Controllers
{
    public class DiscountItem
    {
        public string ProductName { get; set; }
        public int Amount{ get; set; }
        public decimal Price { get; set; }
    }

    public class DiscountViewModel
    {
        public DiscountViewModel()
        {
            if (DiscountItems == null)
            {
                DiscountItems = new List<DiscountItem>()
                {
                    new DiscountItem()
                    {
                        Amount = 1,
                        Price = 200,
                        ProductName = "雞蛋"
                    },
                    new DiscountItem()
                    {
                        Amount = 10,
                        Price = 20,
                        ProductName = "橡皮擦"
                    }
                };
            }
        }

        public List<DiscountItem> DiscountItems { get; set; }

        public decimal TotalPrice { get; set; }

        public List<string> PluginName { get; set; }
    }
}