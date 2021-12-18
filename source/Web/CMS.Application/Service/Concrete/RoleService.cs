using AutoMapper;
using CMS.Application.Models.DTO;

using CMS.Application.Service.Interface;
using CMS.Domain.Entities.Concrete;
using CMS.Domain.UnitofWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Service.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RoleService(IUnitOfWork unitOfWork,
                           IMapper mapper,
                           RoleManager<IdentityRole> roleManager,
                           UserManager<AppUser> userManager)
            
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
           
            this._roleManager = roleManager;
            this._userManager = userManager;
        }


        public async Task Create(CreateRoleDTO model)
        {
            var role = _mapper.Map<IdentityRole>(model);
           IdentityResult result=await _roleManager.CreateAsync(role);
            await _unitOfWork.Commit();


        }
        public IQueryable<IdentityRole> GetRolesList()
        {
            var roles = _roleManager.Roles;

            return roles;
        }

        public async Task Delete(string id)
        {

          await _roleManager.DeleteAsync(await _roleManager.FindByIdAsync(id));
     
        }

        public async Task<UpdateRoleDTO> GetById(string id)
        {
            var Role = await _roleManager.FindByIdAsync(id);

            var model = _mapper.Map<UpdateRoleDTO>(Role);
            return model;
            

        }
        
        public async Task Update(UpdateProductDTO model)
        {
            var role = _mapper.Map<IdentityRole>(model);
            await _roleManager.UpdateAsync(role);
            await _unitOfWork.Commit();
        
        }


        public async Task<bool> isPageExist(string roleName)  //birden fazla girme aynı şeyi
        {
            var Role = await _roleManager.RoleExistsAsync(roleName);
            return Role;
        }



        
     


       

    }
}
