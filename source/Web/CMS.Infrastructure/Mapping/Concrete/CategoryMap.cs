using CMS.Domain.Entities.Concrete;
using CMS.Infrastructure.Mapping.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.Mapping.Concrete
{
   public  class CategoryMap:BaseMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired(true);

            builder.Property(x => x.Slug).IsRequired(true);

             
            base.Configure(builder);
        }

    }
}
