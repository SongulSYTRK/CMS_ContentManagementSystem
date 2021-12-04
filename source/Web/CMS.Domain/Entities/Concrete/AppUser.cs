using CMS.Domain.Entities.Interface;
using CMS.Domain.Enums;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Entities.Concrete
{
    public class AppUser : IdentityUser<int>, IBaseEntity
    {       
        //Normally We define Id into BaseEntity. But IdentityUser.cs have Id so we didnt Id property. We defined int to "Id " (<int>)

        public string FullName { get; set; }
        public string Imagepath { get; set; } = "/images/users/default.jpg";
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Status Status { get; set; }
    }
}
