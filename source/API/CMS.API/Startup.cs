using CMS.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CMS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

           // services.AddControllersWithViews().AddFluentValidation();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("CMSAPI", new OpenApiInfo()
                {
                    Title = "Restful API",
                    Version = "v1.1",
                    Description = "Restful API Training",
                    Contact = new OpenApiContact()
                    {
                        Email = "songulsytrk@gmail.com",
                        Name = "Songul Soyturk",
                        Url = new Uri("https://github.com/Songulsytrk")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT Licanse",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

            //açýklama eklemek için (///getcategory) properties=> build=> xmldocumentationfile iþaretlenir ve aþaðýdaki kodlar yazlýr.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/CMSAPI/swagger.json", "CMS API");
                options.RoutePrefix = string.Empty;

            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
            });

            //Note:Weathercontroller sildikten sonra properties =>debug launch kýsmýda silersen direkt index.html þeklinde sayfayý açar.
        }
    }
}
