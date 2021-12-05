using CMS.Application.Models.VMs;
using CMS.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Application.Models.DTO
{
   public  class CreateProductDTO
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get => "/images/products/default.jpg"; }
        //  public string  CategoryName { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
        public List<GetCategoryVM> Categories { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
