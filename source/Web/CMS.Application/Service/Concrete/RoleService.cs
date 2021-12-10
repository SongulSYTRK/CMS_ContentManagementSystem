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



        
        public async Task<AssignedRoleToUserDTO> GetAssignedRoleToUsers(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            List<AppUser> hasRole = new List<AppUser>();
            List<AppUser> hasNoRole = new List<AppUser>();

            //_userManager.Users vasıtasıyla bütün user'ların listesini bana getirir
            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? hasRole : hasNoRole;
                list.Add(user);
            }

           return  new AssignedRoleToUserDTO
            {
                Role = role,
                HasRole = hasRole,
                HasNoRole = hasNoRole
            };
           

            
        }


        public async Task PostAssignedRoleToUsers(AssignedRoleToUserDTO model)
        {
            IdentityResult result;

            //şayet AddIds[] arayy'i boş gelirse exception yememek için yanında new string[] { } bir array daha yarattık
            foreach (string userId in model.AddIds ?? new string[] { })
            {
                AppUser user = await _userManager.FindByIdAsync(userId); //rol atanacak user'i ıd'sinden yakaladım
                result = await _userManager.AddToRoleAsync(user, model.RoleName); // yukarıda yakaladığımız user'a modelden bize gelen role ismini atadık
            }

            foreach (string userId in model.DeleteIds ?? new string[] { })
            {
                AppUser user = await _userManager.FindByIdAsync(userId); //rolü silinecek user'i ıd'sinden yakaladım
                result = await _userManager.RemoveFromRoleAsync(user, model.RoleName); // yukarıda yakaladığımız user'a modelden bize gelen role ismini sildik
            }

           await _unitOfWork.Commit();
        }

    }
}
