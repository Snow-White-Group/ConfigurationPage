using Microsoft.AspNet.Identity.EntityFramework;

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