using Microsoft.AspNetCore.Mvc;
using ProiectMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectMaster.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductService productService;

        public OrdersController(IProductService productService)
        {
            this.productService = productService;
        }
        public IActionResult Index()
        {
            var products = productService.GetAllProducts();
            return View(products);
        }


        [HttpPost]
        [Route("Add/{id}")]
        public IActionResult Add(int id)
        {
            var shopList = HttpContext.Session.Get<List<int>>(SessionHelper.ShoppingCart);

            if (shopList == null)
                shopList = new List<int>();

            if (!shopList.Contains(id))
                shopList.Add(id);

            HttpContext.Session.Set(SessionHelper.ShoppingCart, shopList);

            return RedirectToAction("Index", "Home", productService.GetAllProducts());
        }
    }

}
