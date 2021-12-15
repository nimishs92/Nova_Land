using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nova_Land.Areas.Identity.Data;
using Nova_Land.Data;
using Nova_Land.Models;
using Nova_Land.ViewModels;
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
        private readonly SignInManager<Nova_LandUser> _signInManager;

        public ProductController(ApplicationDbContext applicationDbContext, UserManager<Nova_LandUser> userManager, SignInManager<Nova_LandUser> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var ProductQuery = from product in _applicationDbContext.Products
                               where product.Id == id
                               select product;
            var ProductList = ProductQuery.ToList();
            var Product = new Product();
            if (ProductList.Count() > 0)
            {
                Product = ProductList[0];
            }
            ProductDetailsViewModel productDetailsVM = new ProductDetailsViewModel
            {
                Product = Product,
                IsItemAdded = false
            };
            return View(productDetailsVM);
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
            searchModelVM.IsItemAdded = false;
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
        [HttpGet]
        [Authorize]
        public ActionResult AddToCartPost(IFormCollection collection)
        {
            
            if (_signInManager.IsSignedIn(User))
            {

            }
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
            searchModelVM.IsItemAdded = true;
            return View("Search", searchModelVM);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddToCartSearch(string Search, string Province, string City, string ProductId)
        {
            ProductSearchViewModel searchModelVM = new ProductSearchViewModel
            {
                SearchParams = new SearchParamsViewModel
                {
                    City = City,
                    Province = Province,
                    Search = Search
                }
            };

            searchModelVM.Products = GetProducts(searchModelVM.SearchParams);

            searchModelVM.IsItemAdded = AddToCart(ProductId);
            return View("Search", searchModelVM);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddToCartDetails(string ProductId)
        {
            Product selectedProduct = _applicationDbContext.Products.FirstOrDefault(product => product.Id == Int32.Parse(ProductId));

            var IsItemAdded = AddToCart(ProductId);

            ProductDetailsViewModel productDetailsVM = new ProductDetailsViewModel
            {
                Product = selectedProduct,
                IsItemAdded = true
            };
            return View("Details", productDetailsVM);
        }

        public bool AddToCart(string ProductId)
        {
            Product selectedProduct = _applicationDbContext.Products.FirstOrDefault(product => product.Id == Int32.Parse(ProductId));

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

            return true;
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
