using AutoMapper;
using CMS.Application.Models.DTO;
using CMS.Application.Models.VMs;
using CMS.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.AutoMapper
{
   public  class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Page, CreatePageDTO>().ReverseMap();
            CreateMap<Page, UpdatePageDTO>().ReverseMap();
            CreateMap<Page, GetPageVM>().ReverseMap();
            CreateMap<UpdatePageDTO, GetPageVM>().ReverseMap();


            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<Category, GetCategoryVM>().ReverseMap();
            CreateMap<UpdateCategoryDTO, GetCategoryVM>().ReverseMap();



            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, GetProductVM>().ReverseMap();
            CreateMap<UpdateProductDTO, GetProductVM>().ReverseMap();

            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, UpdateProfileDTO>().ReverseMap();
            CreateMap<AppUser, GetAppUserVM>().ReverseMap();
        }
    }
}
