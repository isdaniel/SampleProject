using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataListSamlpe.Controllers
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult GetCustomerList()
        {
            return Json(new List<CustomerModel>()
            {
                new CustomerModel()
                {
                    CustomerID = 1,
                    CustomerName = "黃小姐"
                },
                new CustomerModel(){
                    CustomerID = 2,
                    CustomerName = "林先生"

                },
                new CustomerModel(){
                    CustomerID = 3,
                    CustomerName = "陳先生"
                }
            });
        }
    }
}