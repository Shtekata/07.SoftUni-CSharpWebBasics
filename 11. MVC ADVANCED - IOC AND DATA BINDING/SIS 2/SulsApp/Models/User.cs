using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SulsApp.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            Submissions = new HashSet<Submission>();
        }
     
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
