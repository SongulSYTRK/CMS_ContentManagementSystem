﻿using AutoMapper;
using CMS.Application.Models.DTO;
using CMS.Application.Models.VMs;
using CMS.Application.Service.Interface;
using CMS.Domain.Entities.Concrete;
using CMS.Domain.UnitofWork;
using Microsoft.AspNetCore.Identity;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Service.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserService(IUnitOfWork unitOfWork,
                              IMapper mapper, 
                              UserManager<AppUser> userManager, 
                              SignInManager<AppUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }



       
        public async Task<UpdateProfileDTO> GetById(int id)
        {
            var User =await _unitOfWork.AppUserRepository.GetFilteredFirstOrDefault(
                                                          selector: x=> new GetAppUserVM
                                                          { 
                                                           Id=x.Id,
                                                           FullName=x.FullName,
                                                           UserName=x.UserName,
                                                           Email=x.Email,
                                                           Password=x.PasswordHash,
                                                           ConfirmPassword=x.PasswordHash

                                                          },
                                                          expression: x=>x.Id ==id );

            var model = _mapper.Map<UpdateProfileDTO>(User);
            return model;
        }
        public async Task UpdateUser(UpdateProfileDTO model)
        {
            var user = await _unitOfWork.AppUserRepository.GetByDefault(x => x.Id == model.Id);
            if (user != null)
            {
                if (model.Image != null)
                {
                    using var image = Image.Load(model.Image.OpenReadStream());
                    image.Mutate(x => x.Resize(256, 256));
                    image.Save("wwwroot/images/users/" + user.UserName + ".jpg");
                    user.Imagepath = ("/images/users/" + user.UserName + ".jpg");

                    //if (true)
                    //{
                    //    image.Mutate(x => x.Resize(560,560));
                    //    image.Save("wwwroot/images/users/" + user.UserName + ".jpg");
                    //    user.LargeImage = ("/images/users/" + user.UserName + ".jpg");
                    //}
                    //if(true)
                    //{
                    //    image.Mutate(x => x.Resize(256, 256));
                    //    image.Save("wwwroot/images/users/" + user.UserName + ".jpg");
                    //    user.MediumImage = ("/images/users/" + user.UserName + ".jpg");
                    //}
                    //if (true)
                    //{
                    //    image.Mutate(x => x.Resize(60, 60));
                    //    image.Save("wwwroot/images/users/" + user.UserName + ".jpg");
                    //    user.SmallImgae = ("/images/users/" + user.UserName + ".jpg");
                    //}


                    if (model.FullName != null)
                    {
                        user.FullName = model.FullName;
                        _unitOfWork.AppUserRepository.Update(user);
                        await _unitOfWork.Commit();
                    }
                }

                if (model.Password != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                    await _userManager.UpdateAsync(user);
                }

                if (model.UserName != null)
                {
                    await _userManager.SetUserNameAsync(user, model.UserName);
                }
                if (model.Email != null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }
            }
        }


        public async Task<SignInResult> Login(LoginDTO model)
        {
           var result= await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false ,false );
            return result;

        }

        public async Task LogOut()
        {
            await  _signInManager.SignOutAsync();
            
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            var User = _mapper.Map<AppUser>(model);
            var result =await _userManager.CreateAsync(User, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(User, isPersistent:false);
            }
            return result;
        }

       
        public Task<int> UserIdFormName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
