using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Applied_MVC_Concepts_I.Models.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfHours { get; set; }

        public virtual HashSet<ApplicationUser> Users { get; set; }

        public Course()
        {
            Users = new HashSet<ApplicationUser>();
        }
    }
}