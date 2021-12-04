using CMS.Application.Models.DTO;
using CMS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Service.Interface
{
   public interface ICategoryService
    {
        Task Create(CreateCategoryDTO model);
        Task Delete(int id);
        Task Update(UpdateCategoryDTO model);

        Task<UpdateCategoryDTO> GetById(int id);
        Task<List<GetCategoryVM>> GetCategory();
    }
}
