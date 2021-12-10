using CMS.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.DTO
{
    public class UpdateRoleDTO
    {
        public string Id { get; set; }
        public string RoleName { get; set; }

        public List<AppUser> Users { get; set; }
    }
}
