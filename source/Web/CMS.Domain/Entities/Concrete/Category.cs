using CMS.Domain.Entities.Interface;
using CMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Entities.Concrete
{
    public class Category:IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public Status Status { get; set; }

        public List<Product> Products { get; set; }
    }
}
