using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using UIConfiguration.Models;

namespace UIConfiguration.Controllers
{
    [Authorize]
    public class ConfigurationController : Controller
    {
        private static readonly ApplicationDbContext _dbContext = ApplicationDbContext.GetContext();

        private SnowwhiteUser _loggedInUser = ApplicationDbContext.GetUser();

        public ActionResult Index()
        {
            // get all mirrors and send viewmodel with user and mirrors
            return View(this._loggedInUser);
        }

        
        public ActionResult MirrorConfiguration(string id)
        {
            return View(this._loggedInUser);
        }
        
        public ActionResult AddMirror()
        {
            return View();
        }
        
        public ActionResult ShowProfile()
        {
            return View(_loggedInUser);
        }
    }
}