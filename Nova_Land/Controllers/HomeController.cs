using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nova_Land.Data;
using Nova_Land.Models;
using Nova_Land.ViewModels;

namespace Nova_Land.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _applicationDbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            var categoryQuery = from cat in _applicationDbContext.Categories
                                select cat;
            return View(categoryQuery.ToList<Category>());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Payment(int OrderId)
        {
            Order Order = _applicationDbContext.Orders.Include(order => order.OrderLineItems).FirstOrDefault(order => order.ID == OrderId);
            double TotalCost = 0;
            foreach (var item in Order.OrderLineItems)
            {
                TotalCost += item.Price;
            }
            return View(new PaymentViewModel { OrderId = OrderId, TotalPrice = TotalCost});
        }
        [HttpPost]
        public IActionResult PaymentComplete(PaymentViewModel paymentViewModel)
        {
            if (ModelState.IsValid)
            {
                Order order = _applicationDbContext.Orders.FirstOrDefault(o => o.ID == paymentViewModel.OrderId);
                order.Payment = paymentViewModel.Payment;
                order.Status = OrderStatus.COMPLETED;
                _applicationDbContext.SaveChanges();
                return View("PaymentSucess");
            }
            return View("Payment", paymentViewModel);
        }

        public IActionResult PaymentSucess()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
