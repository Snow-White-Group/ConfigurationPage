using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using UIConfiguration.Models;

namespace UIConfiguration.Controllers
{
    [Authorize]
    public class ConfigurationController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        private SnowwhiteUser loggedInUser;

        public ActionResult Index()
        {
            if (this.loggedInUser == null)
            {
                this.loggedInUser = _dbContext.Users.Find(User.Identity.GetUserId());
            }
            
            return View(this.loggedInUser);
        }

        [Authorize]
        public ActionResult ShowProfile()
        {
            return View(loggedInUser);
        }
    }
}