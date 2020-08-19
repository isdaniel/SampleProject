using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExtensionBase;

namespace FileExtensionDemo.Controllers
{
    public class DiscountController : Controller
    {
        private static DiscountViewModel _discountView;
        private FileExtensionService _extensionService;
        public DiscountController()
        {
            _extensionService = new FileExtensionService();
            if (_discountView == null)
            {
                _discountView= new DiscountViewModel();
            }
        }

        // GET: Discount
        public ActionResult Index()
        {
            var plugins = _extensionService.LoadPluginMethod<IDiscount>();
            _discountView.PluginName = plugins.Select(x => x.Description).ToList();
            return View(_discountView);
        }

        [HttpPost]
        public ActionResult DiscountCalc(DiscountCalcViewModel viewModel)
        {
            var plugin  = _extensionService.LoadPluginMethod<IDiscount>()
                .FirstOrDefault(x=>x.Description == viewModel.PluginName);
            var resultPrice = plugin.Instance.GetPrice(viewModel.DiscountItems.Select(x => new DiscountDto()
            {
                Amount = x.Amount,
                Price = x.Price
            }));
            return Json(resultPrice);
        }
    }

    public class DiscountCalcViewModel
    {
        public string PluginName { get; set; }
        public IEnumerable<DiscountItem> DiscountItems { get; set; }
    }
}