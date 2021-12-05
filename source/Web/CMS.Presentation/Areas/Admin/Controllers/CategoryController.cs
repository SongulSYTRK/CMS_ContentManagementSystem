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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Create(model);
                TempData["Success"] = "the catgeoyr has been added";
                return RedirectToAction("List");
            }
            
            TempData["Error"] = "the catgeoyr hasnt been added";
            return View(model);
        }
        public async Task<IActionResult> List()
        {
            return View(await _categoryService.GetCategory());
        }

        public async Task<IActionResult> Update(int id)
        {
            var model =await _categoryService.GetById(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Update(model);
                TempData["Success"] = "the catgeoyr has been update";
                return RedirectToAction("List");
            }

            TempData["Error"] = "the catgeoyr hasnt been update";
            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {

             await _categoryService.Delete(id);
            TempData["Warning"] = "the catgeoyr has been delete";
            return RedirectToAction("List");
        }

    }
}
