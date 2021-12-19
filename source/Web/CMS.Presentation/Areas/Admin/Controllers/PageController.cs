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
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<IActionResult> List() => View(await _pageService.GetPage());


        public async Task<IActionResult> Create() => View();

         [HttpPost]
       public async Task<IActionResult> Create(CreatePageDTO model)
       {
            if(ModelState.IsValid)
            {
                if (await _pageService.isPageExist(model.Slug) != false)
                {
                    TempData["Warning"] = "the page have ";  //   _notificationPartial
                    return View("List");
                }
                await _pageService.Create(model);  
                TempData["Success"] = "the page has been added";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "the page hasnt been added";
                return View();
            }
           
            
       }


        public async Task<IActionResult> Update(int id) => View(await _pageService.GetById(id));
        [HttpPost]
       public async Task<IActionResult> Update(UpdatePageDTO model)
        {
            if (ModelState.IsValid)
            {
                await _pageService.Update(model);
                TempData["Success"] = "the page has been updated";
                return RedirectToAction("List");
            }
            TempData["Error"] = "the page has been updated";
            return View(model);
        }



        public async Task<IActionResult> Delete(int id)
        {
            await _pageService.Delete(id);
            return RedirectToAction("List");
        }
    }
}
