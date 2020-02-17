using SIS.MvcFramework;
using System;

namespace Andreys.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
