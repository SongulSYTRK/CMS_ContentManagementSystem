using CMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.DTO
{
   public  class UpdateCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }


        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
