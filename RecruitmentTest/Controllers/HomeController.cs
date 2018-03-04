using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecruitmentTest.Features;
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
            var query = new Menu.QueryHandler(context).Handle();

            return View(query);
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

        public ActionResult PaymentFailed()
        {
            ViewData["Message"] = "Sorry, your payment failed.";

            return View();
        }

        public ActionResult Order(OrderCommand order, [FromServices] OrderCommandHandler handler)
        {
            var result = handler.Handle(order);

            if (result.PaymentOk)
            {
                ViewData["Message"] = "Thank you. Your order has been placed.";
                return View(result);
            }
            else
            {
                return RedirectToAction("PaymentFailed");
            }
        }        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
