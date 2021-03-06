using CMS.Application.Models.VMs;
using CMS.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.DTO
{
   public  class UpdateProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = "/images/products/default.jpg";
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public List<GetCategoryVM> Categories { get; set; }
        public IFormFile Image { get; set; }
      


        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
