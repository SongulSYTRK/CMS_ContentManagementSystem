using CMS.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Presentation.Models.Components
{
    public class MainMenuViewComponent :ViewComponent
    {
        private readonly IPageService _pageService;
        public MainMenuViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _pageService.GetPage());
        }
    }
}
