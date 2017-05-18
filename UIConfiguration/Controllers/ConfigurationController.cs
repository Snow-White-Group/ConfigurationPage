using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UIConfiguration.Models;

namespace UIConfiguration.Controllers
{
    [Authorize]
    public class ConfigurationController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        private SnowwhiteUser loggedInUser = null;

        public ActionResult Index(string id)
        {
            this.loggedInUser = _dbContext.Users.Find(id);
            
            return View(this.loggedInUser);
        }

        [Authorize]
        public ActionResult ShowProfile()
        {
            return View(loggedInUser);
        }
    }
}