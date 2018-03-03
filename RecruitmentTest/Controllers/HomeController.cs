using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RecruitmentTest.Models;

namespace RecruitmentTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestaurantDbContext context;

        public HomeController(RestaurantDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            List<MenuItem> menuItems = context.MenuItems.ToList();

            return View(menuItems);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Davey P's Italian Restauranty was established in 2016 to produce Spaghetti Code.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public ActionResult PaymentOk()
        {
            ViewData["Message"] = "Thank you. Your order has been placed.";

            return View();
        }

        public ActionResult PaymentFailed()
        {
            ViewData["Message"] = "Sorry, your payment failed.";

            return View();
        }

        public ActionResult Update(int menuItemId, int paymentTypeId)
        {
            var paid = false;

            var paymentGateway = new PaymentGateway();

            switch (paymentTypeId)
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
