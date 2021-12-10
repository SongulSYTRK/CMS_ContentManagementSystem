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
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
          
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
          var model=  _roleService.GetAssignedRoleToUsers(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignedRoleToUsers(AssignedRoleToUserDTO model)
        {
            await _roleService.PostAssignedRoleToUsers(model);

            return RedirectToAction("List");
        }
        #endregion



    }
}

