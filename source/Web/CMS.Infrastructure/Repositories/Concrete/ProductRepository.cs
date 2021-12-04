﻿using CMS.Domain.Entities.Concrete;
using CMS.Domain.Repositories.Interface.EntityTypeRepositories;
using CMS.Infrastructure.Context;
using CMS.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.Repositories.Concrete
{
   public  class ProductRepository: BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext):base(appDbContext)
        {

        }
    }
}
