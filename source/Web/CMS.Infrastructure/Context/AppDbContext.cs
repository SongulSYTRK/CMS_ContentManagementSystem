

using CMS.Domain.Entities.Concrete;
using CMS.Infrastructure.Mapping.Concrete;
using CMS.Infrastructure.SeedData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Text;


namespace CMS.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Page> Pages { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PageMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new AppUserMap());

            builder.ApplyConfiguration(new PageSeeding());
            builder.ApplyConfiguration(new LoginSeeding());
            base.OnModelCreating(builder);
        }


    }
}
