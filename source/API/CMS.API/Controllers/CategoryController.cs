using CMS.Application.Models.DTO;
using CMS.Application.Models.VMs;
using CMS.Application.Service.Interface;
using CMS.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]


    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<GetCategoryVM>> GetCategories()
        {
            return await _categoryService.GetCategory();

        }
        /// <summary>
        /// GetCategoryById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<UpdateCategoryDTO> GetCategoryById(int id)
        {

            return await _categoryService.GetById(id);
        }
        /// <summary>
        /// GetCategoryBySlug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{slug}", Name = "GetBySlug")]
        [ProducesResponseType(200)]
        public async Task<Category> GetCategoryBySlug(string slug)
        {
            return await _categoryService.GetBySlug(slug);
        }


        /// <summary>
        /// DeleteCategory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _categoryService.Delete(id);
            return Ok();

        }



        /// <summary>
        /// CreateCategory
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryDTO model)
        {
            var category = await _categoryService.GetBySlug(model.Slug);
            if (ModelState.IsValid)
            {
                if (category==null)
                {

                    await _categoryService.Create(model);
                    return Ok();
                }

                return BadRequest("We have this category");
            }
           else
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// UpdateCategory
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory([FromBody] UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryService.GetById(model.Id);
                if (category.Slug == model.Slug)
                {
                    ModelState.AddModelError(string.Empty, $"{model.Slug}Categorylaready exist ..");
                    return BadRequest();
                }
                await _categoryService.Update(category);
                return Ok(model);
            }
            return BadRequest();
        }
    }
}
