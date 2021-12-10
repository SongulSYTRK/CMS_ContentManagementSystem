using AutoMapper;
using CMS.Application.Models.DTO;
using CMS.Application.Models.VMs;
using CMS.Application.Service.Interface;
using CMS.Domain.Entities.Concrete;
using CMS.Domain.Enums;
using CMS.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace CMS.Application.Service.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task Create(CreateCategoryDTO model)
        {
           // model.Slug = model.Name.ToLower().Replace(" ", "_");
            var category =  _mapper.Map<Category>(model);
           
            await _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var category =await _unitOfWork.CategoryRepository.GetByDefault(x=>x.Id ==id);
            //_unitOfWork.CategoryRepository.Delete(category);
            category.DeleteDate = DateTime.Now;
            category.Status = Status.Passive;
            await _unitOfWork.Commit();
        }
        public async Task<List<GetCategoryVM>> GetCategory()
        {
            var categoryList = await _unitOfWork.CategoryRepository.GetFilteredFirstOrDefaults(
                                                                                       selector: x => new GetCategoryVM
                                                                                                  {
                                                                                                      Id = x.Id,
                                                                                                      Name=x.Name,
                                                                                                      Slug=x.Slug                                                                                                 
                                                                                                  },
                                                                                       expression: x=>x.Status!=Status.Passive,
                                                                                       orderBy: x=>x.OrderBy(x=>x.Name)
                                                                                       );

            return categoryList;
        }



        public async Task<UpdateCategoryDTO> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetFilteredFirstOrDefault(
                                                                                        selector: x => new GetCategoryVM
                                                                                        {
                                                                                            Id = x.Id,
                                                                                            Name = x.Name,
                                                                                            Slug = x.Slug
                                                                                        },
                                                                                        expression: x => x.Id == id );
           var model= _mapper.Map<UpdateCategoryDTO>(category);
            return model;
        }

       
        public async Task Update(UpdateCategoryDTO model)
        {
            // model.Slug = model.Name.ToLower().Replace(" ", "_");
           model.Slug =Crypto.GenerateSalt().ToLower() + model.Name;  //kriptoladım .Url adresimden nasıl çalıştığımı dışarı gizlemek için
            var category = _mapper.Map<Category>(model);
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.Commit();
        }
        public async Task<Category> GetBySlug(string slug)
        {
            var category = await _unitOfWork.CategoryRepository.GetByDefault(x => x.Slug == slug);
            return category;
        }
    }
}
