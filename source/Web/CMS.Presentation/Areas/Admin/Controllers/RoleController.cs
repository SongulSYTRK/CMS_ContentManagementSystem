using CMS.Application.Models.DTO;

using CMS.Application.Service.Interface;
using CMS.Domain.Entities.Concrete;
using CMS.Presentation.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class RoleController : Controller
    {

        private readonly IRoleService _roleService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RoleController(IRoleService roleService, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleService = roleService;
            _roleManager = roleManager;
            _userManager = userManager;
          
        }
        public IActionResult List()
        {
            return View(_roleService.GetRolesList()); 
        }
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                if (await _roleService.isPageExist(model.RoleName) !=false )
                {
                    TempData["Warning"] = "We have already role ";  //   _notificationPartial
                    return RedirectToAction("List");
                }
                await _roleService.Create(model);
                TempData["Success"] = "The role has been added..!";
                return RedirectToAction("List");
            }
           
                TempData["Error"] = "The role hasn't been added..!";
                
                return View(model);
            
        }


        public async Task<IActionResult> Delete(string id)
        {
            await _roleService.Delete(id);
            return RedirectToAction("List");
        }


        #region Update
        public async Task<IActionResult> Update(string id) => View(await _roleService.GetById(id));
        
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductDTO model)
        {

            {
                if (ModelState.IsValid)
                {
                    await _roleService.Update(model);
                    TempData["Success"] = "the Product has been updated";
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["Error"] = "the product hasnt been update";
                    return View(model);
                }
            }
        }
        #endregion
        #region AssignedRoleToUsers
        public async Task<IActionResult> AssignedRoleToUsers(string id)
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

            return View(new AssignedRoleToUserDTO { Role = role, HasRole = hasRole, HasNoRole = hasNoRole });
        }

        [HttpPost]
        public async Task<IActionResult> AssignedRoleToUsers(AssignedRoleToUserDTO model)
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

            return RedirectToAction("List");
        }
        #endregion



    }
}

