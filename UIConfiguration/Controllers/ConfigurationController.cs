using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using UIConfiguration.Models;
using UIConfiguration.Services;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UIConfiguration.Controllers
{
    [Authorize]
    public class ConfigurationController : Controller
    {
        private readonly ApplicationDbContext _dbContext = ApplicationDbContext.GetContext();
        private static MirrorsUserViewModel _mirrorUserViewModel;
        private SnowwhiteUser _loggedInUser = ApplicationDbContext.GetUser();

        public ActionResult Index()
        {
            _mirrorUserViewModel = new MirrorsUserViewModel()
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
            Mirror targetMirror = _mirrorUserViewModel.Mirrors.FirstOrDefault(x => x.Id.Equals(id));
            _dbContext.Actions.Add(new MirrorAction()
            {
                Id = Guid.NewGuid().ToString(),
                IsDone = false,
                RequestedAt = DateTime.Now,
                TargetAction = ActionForMirror.Record,
                TargetMirror = new MirrorForAction()
                {
                    Id = targetMirror.Id,
                    DisplayName = targetMirror.DisplayName,
                    SecretName = targetMirror.SecretName

                },
                User = new UserForAction()
                {
                    Id = this._loggedInUser.Id,
                    Email = this._loggedInUser.Email,
                    FirstName = this._loggedInUser.FirstName,
                    LastName = this._loggedInUser.LastName
                }
            });

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult GenerateMirrorNames()
        {
            var service = new NameGeneratorService();
            var mirrorNames = service.GenerateMirrorNames();
            _dbContext.Mirrors.Add(new Mirror()
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = mirrorNames.DisplayName,
                SecretName = mirrorNames.SecretName
            });
            _dbContext.SaveChanges();
            return Json(mirrorNames, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult GetPostbox(string secretname)
        {
            var actions = _dbContext.Actions.Where(x => x.TargetMirror.SecretName.Equals(secretname) && !x.IsDone);
            if (actions.Any())
            {
                foreach(var a in actions)
                {
                    a.IsDone = true;
                }
                return Json(actions, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<MirrorAction>(), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult Handshake(string displayName)
        {
            Mirror targetMirror = _dbContext.Mirrors.FirstOrDefault(x => x.DisplayName.Equals(displayName));

            if (targetMirror == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            _loggedInUser.Mirrors.Add(targetMirror);

            _dbContext.Actions.Add(new MirrorAction()
            {
                Id = Guid.NewGuid().ToString(),
                IsDone = false,
                RequestedAt = DateTime.Now,
                TargetAction = ActionForMirror.Handshake,
                TargetMirror = new MirrorForAction()
                {
                    Id = targetMirror.Id,
                    DisplayName = targetMirror.DisplayName,
                    SecretName = targetMirror.SecretName

                },
                User = new UserForAction()
                {
                    Id = this._loggedInUser.Id,
                    Email = this._loggedInUser.Email,
                    FirstName = this._loggedInUser.FirstName,
                    LastName = this._loggedInUser.LastName
                }
            });

            _dbContext.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}