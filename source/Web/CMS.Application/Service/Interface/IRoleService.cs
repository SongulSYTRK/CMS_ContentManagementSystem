using CMS.Application.Models.DTO;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Service.Interface
{
   public  interface  IRoleService
    {
        Task Create(CreateRoleDTO model);
        Task Delete(string id);
        Task Update(UpdateProductDTO model);
        Task<UpdateRoleDTO> GetById(string id);



        IQueryable<IdentityRole> GetRolesList();
        Task<bool> isPageExist(string roleName);


        Task<AssignedRoleToUserDTO> GetAssignedRoleToUsers(string id);


        Task PostAssignedRoleToUsers(AssignedRoleToUserDTO model);

    }
}
