using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UIConfiguration.Models
{
    public class SnowwhiteUsersContext : DbContext
    {
        public SnowwhiteUsersContext()
            : base("name=SnowwhiteUsersContext")
        {
        }

        public DbSet<SnowwhiteUser> SnowwhiteUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SnowwhiteUser>().ToTable("SnowwhiteUsers");
        }
    }
}