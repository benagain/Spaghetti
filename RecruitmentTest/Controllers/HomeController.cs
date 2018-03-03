using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace RecruitmentTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<MenuItem> menuItems;

            using (var context = new RestaurantDbContext())
            {
                menuItems = context.MenuItems.ToList();
            }

            return View(menuItems);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Davey P's Italian Restauranty was established in 2016 to produce Spaghetti Code.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PaymentOk()
        {
            ViewBag.Message = "Thank you. Your order has been placed.";

            return View();
        }

        public ActionResult PaymentFailed()
        {
            ViewBag.Message = "Sorry, your payment failed.";

            return View();
        }

        public ActionResult Update(int menuItemId, int paymentTypeId) 
        {
            var paid = false;

            var paymentGateway = new PaymentGateway();

            switch(paymentTypeId)
            {
                case 1:
                    var dc = new DebitCard("0123 4567 8910 1112");
                    paid = paymentGateway.Pay(dc, 1234, 1.0m);
                    break;
            }

            if (paid)
            {
                return RedirectToAction("PaymentOk");
            }

            return RedirectToAction("PaymentFailed");
        }
    }
}