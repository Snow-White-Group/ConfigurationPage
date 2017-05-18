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
            if (!id.Equals(""))
            {
                loggedInUser = _dbContext.Users.Find(id);
            }
            
            return View(loggedInUser);
        }

        [Authorize]
        public ActionResult ShowProfile()
        {
            return View(loggedInUser);
        }
    }
}