using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoAnLapTrinhWebNC.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DoAnLapTrinhWebNC.Controllers
{
    [Area("ProductManage")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(ProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        // /Areas/ProductManage/Views/Product/Index.cshtml
        public IActionResult Index()
        {
            var product = _productService.OrderBy(p => p.ProductName).ToList();
            return View(product);
        }
    }
}