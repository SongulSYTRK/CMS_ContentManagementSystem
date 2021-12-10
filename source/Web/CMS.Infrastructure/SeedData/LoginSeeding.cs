using CMS.Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.SeedData
{
    public class LoginSeeding : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(

                             new AppUser {FullName="songull", NormalizedUserName = "Songül", NormalizedEmail= "songul@mail.com", Email="songul@mail.com",UserName = "Songül", PasswordHash = "123" },
                             new AppUser { FullName = "ezgii", NormalizedUserName="Ezgi",NormalizedEmail = "ezgi@mail.com", Email = "ezgi@mail.com", UserName = "Ezgi", PasswordHash = "1234" }


                           );
        }
    }
}
