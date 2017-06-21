using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using UIConfiguration.Models;
using UIConfiguration.Services;
using System.Linq;
using System.Collections.Generic;

namespace UIConfiguration.Controllers
{
    [Authorize]
    public class ConfigurationController : Controller
    {
        private static readonly ApplicationDbContext _dbContext = ApplicationDbContext.GetContext();
        private List<MirrorAction> _mirrorActions = new List<MirrorAction>();
        private MirrorsUserViewModel _mirrorUserViewModel;
        private SnowwhiteUser _loggedInUser = ApplicationDbContext.GetUser();

        public ActionResult Index()
        {
            this._mirrorUserViewModel = new MirrorsUserViewModel()
            {
                User = this._loggedInUser,
                Mirrors = _dbContext.Users.Find(this._loggedInUser.Id).Mirrors
            };
            return View(_mirrorUserViewModel);
        }

        
        public ActionResult MirrorConfiguration(string id)
        {
            TargetMirrorUserViewModel mirrorUserVM = new TargetMirrorUserViewModel()
            {
                User = this._loggedInUser,
                Mirror = _mirrorUserViewModel.Mirrors.FirstOrDefault(x => x.Id.Equals(id))
            };
            return View(mirrorUserVM);
        }
        
        public ActionResult AddMirror()
        {
            return View();
        }
        
        public ActionResult ShowProfile()
        {
            return View(_loggedInUser);
        }

        public JsonResult StartRecording(string id)
        {
            this._mirrorActions.Add(new MirrorAction()
            {
                TargetAction = Action.Record,
                TargetMirror = _mirrorUserViewModel.Mirrors.FirstOrDefault(x => x.Id.Equals(id)),
                User = _loggedInUser
            });

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult GenerateMirrorNames()
        {
            var service = new NameGeneratorService();
            return Json(service.GenerateMirrorNames(), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult GetPostbox(string mirrorId)
        {
            return Json(this._mirrorActions, JsonRequestBehavior.AllowGet);
        }
    }
}