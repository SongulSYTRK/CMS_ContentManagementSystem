using CMS.Application.Models.DTO;
using CMS.Application.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
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

        public async Task<IActionResult> Create() => View(new CreateProductDTO() { Categories = await _categoryService.GetCategory() });

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO model)
        {

            if (ModelState.IsValid)
            {
                await _productService.Create(model);
                TempData["Success"] = "The product has been added..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "The product hasn't been added..!";
                model.Categories = await _categoryService.GetCategory();
                return View(model);
            }
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
