using AutoMapper;
using CMS.Application.Models.DTO;
using CMS.Application.Models.VMs;
using CMS.Application.Service.Interface;
using CMS.Domain.Entities.Concrete;
using CMS.Domain.Enums;
using CMS.Domain.UnitofWork;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Service.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }


        public async Task Create(CreateProductDTO model)
        {
            var product = _mapper.Map<Product>(model);

            if (model.Image != null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());
                image.Mutate(x => x.Resize(256, 256));
                image.Save("wwwroot/images/products/" + product.Name + ".jpg");
                product.ImagePath = ("/images/products/" + product.Name + ".jpg");
            }

            await _unitOfWork.ProductRepository.Add(product);

            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var product =await _unitOfWork.ProductRepository.GetByDefault(x=>x.Id==id);

            
            product.DeleteDate = DateTime.Now;
            product.Status = Status.Passive;
            await _unitOfWork.Commit();
        }


        public async Task<List<GetProductVM>> GetProduct()
        {
            var productList =await _unitOfWork.ProductRepository
                                              .GetFilteredFirstOrDefaults(
                                                selector: x=> new GetProductVM
                                                { 
                                                    Id=x.Id,
                                                    Name=x.Name,
                                                    Description=x.Description,
                                                    ImagePath=x.ImagePath,
                                                    CategoryName=x.Category.Name,
                                                    Price=x.Price
                                                
                                                },
                                                expression: x=>x.Status!= Status.Passive,
                                                orderBy: x=>x.OrderBy(x=>x.Name));
            return productList;
        }



        public async Task<UpdateProductDTO> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository
                                           .GetFilteredFirstOrDefault(
                                                   selector: x => new GetProductVM
                                                   {
                                                       Id = x.Id,
                                                       Name = x.Name,
                                                       Description = x.Description,
                                                       ImagePath = x.ImagePath,
                                                       CategoryName = x.Category.Name,
                                                       Price = x.Price

                                                   },
                                                   expression: x => x.Id == id);


            var model = _mapper.Map<UpdateProductDTO>(product);
            model.Categories =await _unitOfWork.CategoryRepository
                                              .GetFilteredFirstOrDefaults
                                                 (selector: x=> new GetCategoryVM
                                                              {
                                                                  Id = x.Id,
                                                                  Name = x.Name,
                                                              },
                                                  expression: x=>x.Status !=Status.Passive);

            return model;
                                                                                                                                                          
        }

       

        public async Task Update(UpdateProductDTO model)
        {
            var product = _mapper.Map<Product>(model);

            _unitOfWork.ProductRepository.Update(product);

            await _unitOfWork.Commit();
        }

        public async Task<List<GetProductVM>> GetProductsByCategory(int categoryId)
        {
            var productsByCategoryList = await _unitOfWork.ProductRepository.GetFilteredFirstOrDefaults(
                selector: x => new GetProductVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                    CategoryName = x.Category.Name,
                    Description = x.Description

                },
                expression: x => x.CategoryId == categoryId && x.Status != Status.Passive,
                orderBy: x => x.OrderBy(z => z.Name));

            return productsByCategoryList;
        }
    }
}
