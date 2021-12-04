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

namespace CMS.Application.Service.Concrete
{
    public class PageService : IPageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }



        public async Task Create(CreatePageDTO model)
        {
            var page = _mapper.Map<Page>(model);
            await  _unitOfWork.PageRepository.Add(page);
            await _unitOfWork.Commit();
        }



        public async Task Delete(int id)
        {
           var page=await _unitOfWork.PageRepository.GetByDefault(x=>x.Id==id);
            // _unitOfWork.PageRepository.Delete(page);
            page.DeleteDate = DateTime.Now;
            page.Status = Status.Passive;
            await _unitOfWork.Commit();
        }




        public async Task<UpdatePageDTO> GetById(int id)
        {
            var page = await _unitOfWork.PageRepository.GetFilteredFirstOrDefault(
                                                         selector:x=> new GetPageVM
                                                                    { 
                                                                     Id=x.Id,
                                                                     Title=x.Title,
                                                                     Content=x.Content,
                                                                     Slug=x.Slug
                                                                    },
                                                         expression: x=>x.Id==id);


          var model= _mapper.Map<UpdatePageDTO>(page);
           return model;
        }

      

        public async Task Update(UpdatePageDTO model)
        {
            var page = _mapper.Map<Page>(model);
           _unitOfWork.PageRepository.Update(page);
            await _unitOfWork.Commit();
        }




        public async Task<List<GetPageVM>> GetPage()
        {
            var pageList = await _unitOfWork.PageRepository.GetFilteredFirstOrDefaults(
                                                        selector: x => new GetPageVM
                                                        {
                                                            Id = x.Id,
                                                            Title = x.Title,
                                                            Content = x.Content,
                                                            Slug = x.Slug
                                                        },
                                                        expression: x => x.Status != Status.Passive,
                                                        orderBy: x => x.OrderBy(x => x.Title));
            return pageList;
        }
    }
}
