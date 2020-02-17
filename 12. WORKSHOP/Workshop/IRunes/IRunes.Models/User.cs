using SIS.MvcFramework;
using System;

namespace IRunes.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
