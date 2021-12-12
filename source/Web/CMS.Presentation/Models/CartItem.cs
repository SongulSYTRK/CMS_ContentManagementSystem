using CMS.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Presentation.Models
{
    public class CartItem
    {
        public CartItem()
        {

        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string  Image { get; set; }



        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Quantity = 1;
            Price = product.Price;
            Image = product.ImagePath;
        }
    }
}
