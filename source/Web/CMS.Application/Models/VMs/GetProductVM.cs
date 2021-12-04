using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.VMs
{
   public class GetProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = "/images/products/default.jpg";
        public string CategoryName { get; set; }
    }
}
