using CMS.Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

using System.Text;

namespace CMS.Infrastructure.SeedData
{
    public class PageSeeding : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasData(

                            new Page { Id = 1, Title = "Home", Content = "home", Slug = "home" },
                            new Page { Id = 2, Title = "About Us", Content = "about us", Slug = "about_us" },
                            new Page { Id = 3, Title = "Contect", Content = "contect", Slug = "contect" }

                          );
        }
    }
}
