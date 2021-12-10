using CMS.Application.Models.DTO;
using CMS.Application.Models.VMs;
using CMS.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Service.Interface
{
   public interface IPageService
    {
        Task Create(CreatePageDTO model);
        Task Delete(int id);
        Task Update(UpdatePageDTO model);

        Task<bool> isPageExist(string slug);
        Task<UpdatePageDTO> GetById(int id);
        Task<List<GetPageVM>> GetPage();


        Task<Page> GetBySlug(string slug);


    }
}
