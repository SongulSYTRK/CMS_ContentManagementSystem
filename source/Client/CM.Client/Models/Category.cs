using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Client.Models
{
    public enum Status { Active = 1, Modified = 2, Passive = 3 }
    public class Category
    {
        public int Id { get; set; }

        [RegularExpression(@"[a-zA-Z ]+$", ErrorMessage = " Only letters are allowed")]
        public string Name { get; set; }
        public string Slug => Name.ToLower().Replace(" ", "-");
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public Status Status { get; set; }

    }
}
