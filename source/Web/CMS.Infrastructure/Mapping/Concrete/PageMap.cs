using CMS.Domain.Entities.Concrete;
using CMS.Infrastructure.Mapping.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.Mapping.Concrete
{
  public  class PageMap:BaseMap<Page>
    {
        public override void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Title).HasMaxLength(150).IsRequired(true);
            builder.Property(x => x.Content).HasMaxLength(500).IsRequired(true);
            builder.Property(x => x.Slug).HasMaxLength(150).IsRequired(true);
            base.Configure(builder);
        }
    }
}
