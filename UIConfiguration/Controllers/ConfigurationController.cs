using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using UIConfiguration.Models;

namespace UIConfiguration.Controllers
{
    [Authorize]
    public class ConfigurationController : Controller
    {
        private static ApplicationDbContext _dbContext = new ApplicationDbContext();

        private SnowwhiteUser loggedInUser = null;

        public ActionResult Index()
        {
            if (this.loggedInUser == null)
            {
                this.loggedInUser = _dbContext.Users.Find(User.Identity.GetUserId());
            }

            // get all mirrors and send viewmodel with user and mirrors
            return View(this.loggedInUser);
        }

        
        public ActionResult ConfigurateMirror(string id)
        {
            return View();
        }
        
        public ActionResult AddMirror()
        {
            return View();
        }
        
        public ActionResult ShowProfile()
        {
            return View(loggedInUser);
        }
    }
}