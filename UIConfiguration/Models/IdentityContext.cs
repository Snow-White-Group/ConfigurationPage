using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace UIConfiguration.Models
{
    public class ApplicationDbContext : IdentityDbContext<SnowwhiteUser>
    {
        private static ApplicationDbContext _context;

        public ApplicationDbContext()
            : base("SnowwhiteUsersContext")
        {
        }

        public DbSet<Mirror> Mirrors { get; set; }
        public DbSet<MirrorAction> Actions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SnowwhiteUser>().HasKey(x => x.Id);
            modelBuilder.Entity<Mirror>().ToTable("Mirrors");
            modelBuilder.Entity<MirrorAction>().ToTable("Actions");
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