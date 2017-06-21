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
        private List<MirrorAction> _mirrorActions = new List<MirrorAction>();
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
            this._mirrorActions.Add(new MirrorAction()
            {
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
            var actions = this._mirrorActions.Where(x => x.TargetMirror.SecretName.Equals(secretname));
            if (actions.Any())
            {
                foreach(var a in actions)
                {
                    this._mirrorActions.Remove(a);
                }
                return Json(actions, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult Handshake(string displayName)
        {
            Mirror targetMirror = _dbContext.Mirrors.FirstOrDefault(x => x.DisplayName.Equals(displayName));

            if (targetMirror == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            this._mirrorActions.Add(new MirrorAction()
            {
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

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}