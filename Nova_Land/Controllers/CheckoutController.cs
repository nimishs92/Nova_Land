using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nova_Land.Areas.Identity.Data;
using Nova_Land.Data;
using System.Linq;

namespace Nova_Land.Controllers
{
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Nova_LandUser> _userManager;
        public CheckoutController(ApplicationDbContext applicationDbContext, UserManager<Nova_LandUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        // GET: CheckoutController
        public ActionResult Index()
        {
            Nova_LandUser applicationUser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();

            var UserCart = _applicationDbContext.Orders.Include(order => order.OrderLineItems)
                            .ThenInclude(orderLi => orderLi.Product)
                            .FirstOrDefault(order => order.IsCart == true && order.User.Id == applicationUser.Id);

            return View(UserCart);
        }

        // GET: CheckoutController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CheckoutController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckoutController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckoutController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CheckoutController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckoutController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CheckoutController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
