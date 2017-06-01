﻿using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UIConfiguration.Models
{
    public class ApplicationDbContext : IdentityDbContext<SnowwhiteUser>
    {
        private static ApplicationDbContext _context;

        public ApplicationDbContext()
            : base("SnowwhiteUsersContext")
        {
        }

        public static ApplicationDbContext Create()
        {
            _context = new ApplicationDbContext();
            return _context;
        }

        public static ApplicationDbContext GetContext()
        {
            return _context;
        }

        public static SnowwhiteUser GetUser()
        {
            ApplicationDbContext _context = ApplicationDbContext.GetContext();
            return _context.Users.Find(HttpContext.Current.User.Identity.GetUserId());
        }
    }
}