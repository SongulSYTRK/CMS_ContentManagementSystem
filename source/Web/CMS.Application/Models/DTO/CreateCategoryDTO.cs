using CMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Application.Models.DTO
{
   public  class CreateCategoryDTO
    {
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only letters are allowed")]
        public string Name { get; set; }

        // public string Slug { get; set; }
        public string Slug => Name.ToLower().Replace(" ", "_");


        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
