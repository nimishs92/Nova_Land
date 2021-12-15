using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nova_Land.Areas.Identity.Data;
using Nova_Land.Data;
using Nova_Land.Models;
using Nova_Land.ViewModels;
using Nova_Land.Views.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nova_Land.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Nova_LandUser> _userManager;

        public ProductController(ApplicationDbContext applicationDbContext, UserManager<Nova_LandUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: ProductController/Search
        [HttpPost]
        public ActionResult Search(IFormCollection collection)
        {
            ProductSearchViewModel searchModelVM = new ProductSearchViewModel
            {
                SearchParams = new SearchParamsViewModel
                {
                    City = collection["City"],
                    Province = collection["Province"],
                    Search = collection["Search"]
                }
            };

            searchModelVM.Products = GetProducts(searchModelVM.SearchParams);

            return View(searchModelVM);
        }

        private List<Product> GetProducts(SearchParamsViewModel searchParamsViewModel)
        {
            var ProductsQuery = from product in _applicationDbContext.Products
                                select product;

            if (!string.IsNullOrEmpty(searchParamsViewModel.Search))
            {
                ProductsQuery = ProductsQuery.Where(p => p.Name.Contains(searchParamsViewModel.Search));
            }

            if (!string.IsNullOrEmpty(searchParamsViewModel.Province))
            {
                ProductsQuery = ProductsQuery.Where(p => p.Locations.Where(loc => loc.Province.Equals(searchParamsViewModel.Province)).Count() > 0);
            }

            if (!string.IsNullOrEmpty(searchParamsViewModel.City))
            {
                ProductsQuery = ProductsQuery.Where(p => p.Locations.Where(loc => loc.City.Equals(searchParamsViewModel.City)).Count() > 0);
            }

            return ProductsQuery.ToList<Nova_Land.Models.Product>();
        }

        [HttpPost]
        public ActionResult AddToCart(IFormCollection collection)
        {
            ProductSearchViewModel searchModelVM = new ProductSearchViewModel
            {
                SearchParams = new SearchParamsViewModel
                {
                    City = collection["City"],
                    Province = collection["Province"],
                    Search = collection["Search"]
                }
            };

            searchModelVM.Products = GetProducts(searchModelVM.SearchParams);

            Product selectedProduct = searchModelVM.Products.FirstOrDefault(product => product.Id == Int32.Parse(collection["ProductId"]));

            Nova_LandUser applicationUser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();

            OrderLineItem orderLineItem = new OrderLineItem
            {
                Product = selectedProduct,
                Price = selectedProduct.UnitPrice
            };

            // Check if user has a cart already

            var UserCart = _applicationDbContext.Orders.Include(order => order.OrderLineItems).FirstOrDefault(order => order.IsCart == true && order.User.Id == applicationUser.Id);
            if (UserCart != null)
            {
                // The user has a cart
                UserCart.OrderLineItems.Add(orderLineItem);
            }
            else
            {
                // Create a cart for the user.
                _applicationDbContext.Orders.Add(new Order
                {
                    IsCart = true,
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.NEW,
                    User = applicationUser,
                    OrderLineItems = new List<OrderLineItem>() { orderLineItem }
                });
            }

            _applicationDbContext.SaveChanges();
            return View("Search", searchModelVM);
        }

        // POST: ProductController/Create
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

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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
