using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.DTO
{
   public  class RegisterDTO
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
