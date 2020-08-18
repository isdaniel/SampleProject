using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileExtensionDemo.Controllers
{
    public class DiscountController : Controller
    {
        private static DiscountViewModel _discountView;
        
        public DiscountController()
        {
            if (_discountView == null)
            {
                _discountView= new DiscountViewModel();
            }

           
        }

        // GET: Discount
        public ActionResult Index()
        {
            return View(_discountView);
        }
    }
}