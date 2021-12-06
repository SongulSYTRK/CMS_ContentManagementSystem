using CMS.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Presentation.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        public PageController(IPageService pageService) => _pageService = pageService;
        public async Task<IActionResult> Index(string slug)
        {
            var page = await _pageService.GetBySlug(slug);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }
    }
}
