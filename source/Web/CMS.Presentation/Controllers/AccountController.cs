using CMS.Application.Models.DTO;
using CMS.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CMS.Application.Extensions;

namespace CMS.Presentation.Controllers
{
    [Authorize, AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            this._appUserService = appUserService;
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home"); 
            }
            return View();
        }
        [AllowAnonymous,HttpPost]
        public async  Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Register(model); 

                if (result.Succeeded)  //if register successed, Homapage open authtomiticially
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
               
                foreach (var item in result.Errors)
                {
          
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(model);
        }

        [AllowAnonymous]

        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> Login(LoginDTO model , string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result =await _appUserService.Login(model);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);   //kaldığımız page 'e geri dönüş yapacak
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt..!");

            }
            return View(model);

        }

        private IActionResult RedirectToLocal(string  returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _appUserService.LogOut();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

          public async Task<IActionResult> Edit(string userName)
        {
            if (userName == User.Identity.Name)
            {
                var user = await _appUserService.GetById(User.GetUserId()); //Authenticate olmuş yani sisteme login olmuş kullanıcıyı temsil eden "User"ın username'minden Asp .Net Core içerisinde gömülü olarak bulunan ClaimsPrinciple sınıfına custum bir extension method yazarak Id'sini yakaladım.

                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.UpdateUser(model);
                TempData["Success"] = "Your has been profile updated..!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                TempData["Error"] = "Your profile hasn't been updated..!";
                return View(model);
            }
        }



        public async Task<IActionResult> ProfileDetail(string userName)
        {
            if (userName == User.Identity.Name)
            {
                var user = await _appUserService.GetByUser(User.GetUserId());
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }




    }
}
