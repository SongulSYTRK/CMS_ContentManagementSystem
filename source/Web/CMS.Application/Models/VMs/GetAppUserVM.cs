using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.VMs
{
    public class GetAppUserVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ImagePath { get; set; }
    }
}
