using CMS.Application.Models.DTO;
using CMS.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService,
                                 ICategoryService categoryService)


        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> List()
        {
            return View(await _productService.GetProduct());
        }

        public async Task<IActionResult> Create()
        {
            CreateProductDTO model = new CreateProductDTO();
            model.Categories =await _categoryService.GetCategory();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                await _productService.Create(model);
                TempData["Success"] = "Product has been added";
                return RedirectToAction("List");
            }
            TempData["Error"] = "Product hasnt been added";
            return View(model);
        }

        #region Update
        public async Task<IActionResult> Update(int id)
        {
            var model = await _productService.GetById(id);
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductDTO model)
        {

            {
                if (ModelState.IsValid)
                {
                    await _productService.Update(model);
                    TempData["Success"] = "the Product has been updated";
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["Error"] = "the product hasnt been update";
                    return View(model);
                }
            }
        }
        #endregion
        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);

            return RedirectToAction("List");
        }
        #endregion
    }
}
