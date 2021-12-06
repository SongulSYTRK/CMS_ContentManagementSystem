using CMS.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Presentation.Models.Components
{
    public class CategoriesViewComponent:ViewComponent 
    {
        private readonly ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService) => _categoryService = categoryService;
        public async Task<IViewComponentResult> InvokeAsync() => View(await _categoryService.GetCategory());
    }
}
