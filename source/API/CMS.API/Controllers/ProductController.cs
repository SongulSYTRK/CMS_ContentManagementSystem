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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<GetProductVM>> GetProducts() => Ok(await _productService.GetProduct());

        /// <summary>
        /// Get By ıd 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductsByID(int id) => Ok(await _productService.GetById(id));

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _productService.Delete(id);
            return Ok();

        }



        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("single-image")]
        public async Task<ActionResult<Product>> CreateCategory([FromBody] CreateProductDTO model, IFormFile file)
        {
            
           
            if (ModelState.IsValid)
            {
                await _productService.Create(model);
               
                return Ok();
            }
            else
            {
                model.Categories = await _categoryService.GetCategory();
                return BadRequest();
            }
        }
        
   

         /// <summary>
         /// Update Products
         /// </summary>
         /// <param name="model"></param>
         /// <returns></returns>
         [HttpPut]
        public async Task<ActionResult<Product>> UpdateCategory([FromBody] UpdateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.GetById(model.Id);
                if (product.Id == model.Id)
                {
                    ModelState.AddModelError(string.Empty, $"{model.Id}Product exist ..");
                   
                    return BadRequest();
                }
                await _productService.Update(product);
                return Ok(model);
            }
            return BadRequest();
        }

        
        
    }
}
