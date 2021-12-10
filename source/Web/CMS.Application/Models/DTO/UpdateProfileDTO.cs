using CMS.Application.Extension;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Application.Models.DTO
{
   public  class UpdateProfileDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ImagePath { get => "/images/users/default.jpg"; }



        [NotMapped]
        [FileExtension]
        public IFormFile Image { get; set; }
    }
}
