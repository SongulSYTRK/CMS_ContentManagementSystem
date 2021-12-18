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
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;
        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        /// <summary>
        /// All Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetPageVM>>> GetPage() => await _pageService.GetPage();

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id) => Ok(await _pageService.GetById(id));



        /// <summary>
        /// GetPageBySlug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{slug}", Name = "GetPageBySlug")]
        [ProducesResponseType(200)]
        public async Task<Page> GetPageBySlug(string slug) => await _pageService.GetBySlug(slug);

        /// <summary>
        /// Create Page
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Page>> CreatePage([FromBody] CreatePageDTO model)
        {

            var category = await _pageService.GetBySlug(model.Slug);
            if (ModelState.IsValid)
            {
                if (category == null)
                {

                    await _pageService.Create(model);
                    return Ok();
                }

                return BadRequest("We have this page");
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// UpdatePAge
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Page>> UpdatePage([FromBody] UpdatePageDTO model)
        {
            if (ModelState.IsValid)
            {
                var category = await _pageService.GetById(model.Id);
                if (category.Slug == model.Slug)
                {
                    ModelState.AddModelError(string.Empty, $"{model.Slug}Categorylaready exist ..");
                    return BadRequest();
                }
                await _pageService.Update(category);
                return Ok(model);
            }
            return BadRequest();
        }
    }



}
