using CMS.Application.Service.Interface;
using CMS.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }



        public async Task<IActionResult> Index()
        {
            var product = await _productService.GetProduct();
            return View(product);
        }

        public async Task<IActionResult> ProductsByCategory(string categorySlug)
        {
           
            Category category = await _categoryService.GetBySlug(categorySlug);

            if (category == null)
                return RedirectToAction("Index");


          
            var productList = await _productService.GetProductsByCategory(category.Id);

            return View(productList);
        }
    }
}
