using System.IO;
using System.Linq;
using DoAnLapTrinhWebNC.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DoAnLapTrinhWebNC.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly ProductService _productService;
        public FirstController(ILogger<FirstController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        public string Index()
        {
            return "Xin chao cac ban";
        }


        public void Nothing()
        {
            _logger.LogInformation("Nothing Action");
        }

        public IActionResult Redme()
        {
            var content = @"Xin chao ca ban
            
            
            minh la khanh";

            return Content(content, "text/plain");
        }

        public IActionResult Bird()
        {
            string filePath = Path.Combine(Startup.ContentRootPath, "Files", "pic1.jpg");
            var bytes = System.IO.File.ReadAllBytes(filePath);

            return File(bytes, "image/jpg");
        }

        public IActionResult ProductPrice()
        {
            return Json(
              new
              {
                  ProductName = "Iphone X",
                  Price = 500
              }
            );
        }

        public IActionResult Privacy()
        {
            var url = Url.Action("Privacy", "Home");
            _logger.LogInformation("chuyen huong den" + url);
            return LocalRedirect(url);
        }

        public IActionResult HelloView(string username)
        {
            if (string.IsNullOrEmpty(username))
                username = "Khach";
            return View("/MyView/hello.cshtml", username);
        }
        public IActionResult HelloView2(string username)
        {
            if (string.IsNullOrEmpty(username))
                username = "Khach";
            return View("hello2", username);
        }
        public IActionResult HelloView3(string username)
        {
            if (string.IsNullOrEmpty(username))
                username = "Khach";
            // /View/First/HelloView3
            return View((object)username);
        }

        [TempData]
        public string StatusMessage { set; get; }

        [AcceptVerbs("POST", "GET")]
        public IActionResult ViewProduct(int? id)
        {
            var product = _productService.Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                StatusMessage = "San pham khong ton tai";
                //TempData["StatusMessage"] = "San pham khong ton tai";
                return Redirect(Url.Action("Index", "Home"));
            }


            //Model
            // return View(product);

            //ViewData
            // this.ViewData["product"] = product;
            // ViewData["title"] = product.ProductName;
            // return View("ViewProduct2");

            //ViewBag
            ViewBag.product = product;
            return View("ViewProduct3");
        }
    }
}