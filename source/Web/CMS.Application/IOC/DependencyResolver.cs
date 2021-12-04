using Autofac;  //For Module
using AutoMapper;
using CMS.Application.AutoMapper;
using CMS.Application.Models.DTO;
using CMS.Application.Service.Concrete;
using CMS.Application.Service.Interface;
using CMS.Application.Validation.FluentValidation;
using CMS.Domain.UnitofWork;
using CMS.Infrastructure.UnitOfWork;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.IOC
{
   public class DependencyResolver :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            

            #region unitofwork

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            #endregion

            #region service
            builder.RegisterType<PageService>().As<IPageService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();

            #endregion


            #region fluentvalidation
            builder.RegisterType<LoginDTOValidation>().As<IValidator<LoginDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<RegisterDTOValidation>().As<IValidator<RegisterDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateProfileDTOValidation>().As<IValidator<UpdateProfileDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<CreateCategoryDTOValidation>().As<IValidator<CreateCategoryDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateCategoryDTOValidation>().As<IValidator<UpdateCategoryDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<CreatePageDTOValidation>().As<IValidator<CreatePageDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdatePageDTOValidation>().As<IValidator<UpdatePageDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<CreateProductDTOValidation>().As<IValidator<CreateProductDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateProductDTOValidation>().As<IValidator<UpdateProductDTO>>().InstancePerLifetimeScope();
            #endregion


            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<Mapping>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            #endregion

            base.Load(builder);

        }
    }
}
