using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace UIConfiguration.Models
{
    public class ApplicationDbContext : IdentityDbContext<SnowwhiteUser>
    {
        public ApplicationDbContext()
            : base("SnowwhiteUsersContext")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}