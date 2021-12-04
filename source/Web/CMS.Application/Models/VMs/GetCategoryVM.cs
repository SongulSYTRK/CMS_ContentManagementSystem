using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.VMs
{
    public class GetCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
