using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.VMs
{
   public  class GetPageVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
    }
}
