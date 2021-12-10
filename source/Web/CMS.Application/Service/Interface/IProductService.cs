using CMS.Application.Models.DTO;
using CMS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Service.Interface
{
   public interface IProductService
    {

        Task Create(CreateProductDTO model);
        Task Delete(int id);
        Task Update(UpdateProductDTO model);

        Task<UpdateProductDTO> GetById(int id);
        Task<List<GetProductVM>> GetProduct();
        Task<List<GetProductVM>> GetProductsByCategory(int categoryId);
    }
}
