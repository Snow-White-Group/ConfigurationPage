﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Text;

namespace UIConfiguration.Models
{
    /* TODO */
    /*
        - verification mail does not work. Provider problem?
        - need email adress for this!

        - passwort reset

        - forgot password | secret question?

        - profilepage
         
      */

    public class AccountController : Controller
    {
        // application- and signInManger are required for the identity management. They handle the authentication and the sessions
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        // database context
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        // user object which is logged in
        private SnowwhiteUser loggedInUser = null;

        // check variable for the mirror to see when a user want to record his/her voice on the mirror
        private bool desireRecord = false;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    this.loggedInUser = this._dbContext.Users.FirstOrDefault(x => x.Email.Equals(model.Email));
                    return RedirectToAction("Index", "Configuration", new { id = this.loggedInUser.Id });
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("NotVerficated","Account");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            // if model is valid
            if (ModelState.IsValid)
            {
                // new user
                // this data have to be set by hand. The rest will be filled automatically
                var user = new SnowwhiteUser {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Thumbnail = SaveFile(model.Thumbnail),
                    RegistrationDate = DateTime.Now,
                    UserName = model.Email,
                    Email = model.Email
                };

                // user will be created and saved in the database | identity entity framework save this => no context.SaveChanges() needed
                var result = await UserManager.CreateAsync(user, model.Password);

                // result = success?
                if (result.Succeeded)
                {
                    SendVerificationEmail(user);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult NotVerificated()
        {
            return View();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public JsonResult SetDesireRecord()
        {
            this.desireRecord = !this.desireRecord;
            return Json(this.desireRecord ? "success" : "failed", JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult DesireRecording()
        {
            if (this.desireRecord)
            {
                SetDesireRecord();
                return Json(this.loggedInUser, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        private Task SendVerificationEmail(SnowwhiteUser user)
        {
            SnowwhiteUser userForMail = loggedInUser ?? user;
            string body = "Hello " + userForMail.FirstName + " " + userForMail.LastName + "!";
            body += "<br /><a href = '" + Url.Action("Verify", "Account", new { id = userForMail.Id }) + "'>Click here to activate your account.</a>";
            body += "<br /><br/> Thank you and have fun with enjoying your smart mirror!";
            body += "<br /><br/> Your Snowwhite-Team";

            using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailAddress"], userForMail.Email))
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.web.de";
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["EmailAddress"].ToString(), ConfigurationManager.AppSettings["EmailPW"].ToString());
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                mm.Body = body;
                mm.BodyEncoding = Encoding.UTF8;
                mm.IsBodyHtml = true;
                smtp.Send(mm);
            }

            return Task.FromResult(0);
        }

        private string SaveFile(string path)
        {
            if (path != null)
            {
                try
                {
                    HttpPostedFileBase profileImage = Request.Files["thumbnail"];
                    string targetLocation = Server.MapPath("~/Content/Images/ProfilePictures/");
                    if (profileImage.ContentLength > 0)
                    {
                        //Determining file name. You can format it as you wish.
                        string FileName = profileImage.FileName;
                        //Determining file size.
                        int FileSize = profileImage.ContentLength;
                        //Creating a byte array corresponding to file size.
                        byte[] FileByteArray = new byte[FileSize];
                        //Posted file is being pushed into byte array.
                        profileImage.InputStream.Read(FileByteArray, 0, FileSize);
                        //Uploading properly formatted file to server.
                        profileImage.SaveAs(targetLocation + FileName);

                        return FileName;
                    }
                }
                catch (Exception e)
                {
                    return "";
                }
            }
            return "";
        }
    }
}