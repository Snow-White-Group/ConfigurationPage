using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UIConfiguration.Services;

namespace UIConfiguration.Models
{
    /* TODO */
    /*
        - passwort reset => test

        - forgot password => test
         
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

        // smtp client for sending emails
        private static SmtpClient smtpClient = new EmailService().smtpClient;

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
                    SnowwhiteUser loggedIn = _dbContext.Users.FirstOrDefault(x => x.Email.Equals(model.Email));
                    //var identity = await UserManager.CreateIdentityAsync(loggedIn, DefaultAuthenticationTypes.ApplicationCookie); 
                    this.loggedInUser = this._dbContext.Users.FirstOrDefault(x => x.Email.Equals(model.Email));
                    return RedirectToAction("Index", "Configuration");
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

        // log off mechanism
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // show view when user ís not verificated
        [AllowAnonymous]
        public ActionResult NotVerificated()
        {
            return View();
        }

        // send verification email
        private void SendVerificationEmail(SnowwhiteUser user)
        {
            SnowwhiteUser userForMail = loggedInUser ?? user;
            string body = "<img src='/Content/snowwhite-logo48.png'/>";
            body += "<h2 style='color: #2196F; font-weight: 300; font-size: 6em; 2.92rem;'>Hello " + userForMail.FirstName + " " + userForMail.LastName + "!</h2>";
            //body += "<br /><a href = '" + Url.Action("Verify", "Account", new { id = userForMail.Id }) + "'>Click here to activate your account.</a>";
            body += "<br /><a href = 'http://snowwhite-configurationpage.azurewebsites.net/Account/Verify?id=" + userForMail.Id +"'>Click here to activate your account.</a>";
            body += "<br /><br/> Thank you and have fun and enjoy your smart mirror!";
            body += "<br /><br/><span style='color: #2196F3; font-weight: 100; font-size: 3em;'>Your Snowwhite-Team</span>";
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(ConfigurationManager.AppSettings["EmailAddress"]);
                mail.To.Add(userForMail.Email);
                mail.Subject = "Verificatation for " + userForMail.FirstName + " " + userForMail.LastName;

                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;

                smtpClient.Send(mail);
            } catch(Exception e)
            {
                string txt = e.Message;
            }
        }

        // on the verification mail the user  will be redirected to this method
        public ActionResult Verify(string id)
        {
            SnowwhiteUser user = _dbContext.Users.FirstOrDefault(x => x.Id.Equals(id));
            user.EmailConfirmed = true;
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Configuration");
        }

        // forgot password
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // send email to set the new password
        public async Task<JsonResult> SendForgotPasswordMail(string email)
        {
            SnowwhiteUser userForMail = _dbContext.Users.FirstOrDefault(x => x.Email.Equals(email));
            string body = "<img src='/Content/snowwhite-logo48.png'/>";
            body += "<h2 style='color: #2196F3; font-weight: 300; font-size: 6em;'>Hello " + userForMail.FirstName + " " + userForMail.LastName + "!</h2>";
            //body += "<br /><a href = '" + Url.Action("ResetPassword", "Account", new { id = userForMail.Id, forgot = true }) + "'>Click here to reset your password.</a>";
            body += "<br /><a href = 'http://snowwhite-configurationpage.azurewebsites.net/Account/ForgotPassword?id=" + userForMail.Id +"'>Click here to set a new password.</a>";
            body += "<br /><br/><span style='color: #2196F3; font-weight: 100; font-size: 3em;'>Your Snowwhite-Team</span>";

            using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailAddress"], userForMail.Email))
            {
                MailAddress sender = new MailAddress(ConfigurationManager.AppSettings["EmailAddress"], "Snowwhite-Team", Encoding.UTF8);
                mm.Subject = "Resetting for " + userForMail.FirstName + " " + userForMail.LastName;

                mm.Body = body;
                mm.BodyEncoding = Encoding.UTF8;
                mm.IsBodyHtml = true;

                smtpClient.SendAsync(mm, userForMail.Id);
            }
            return Json(await Task.FromResult(0), JsonRequestBehavior.AllowGet);
        }

        // send email to reset the old password
        public async Task<JsonResult> SendResetPasswordMail(string email)
        {
            SnowwhiteUser userForMail = _dbContext.Users.FirstOrDefault(x => x.Email.Equals(email));
            string body = "<img src='/Content/snowwhite-logo48.png'/>";
            body += "<h2 style='color: #2196F3; font-weight: 300; font-size: 6em;'>Hello " + userForMail.FirstName + " " + userForMail.LastName + "!</h2>";
            //body += "<br /><a href = '" + Url.Action("ResetPassword", "Account", new { id = userForMail.Id, forgot = true }) + "'>Click here to reset your password.</a>";
            body += "<br /><a href = 'http://snowwhite-configurationpage.azurewebsites.net/Account/ResetPassword?id=" + userForMail.Id + "'>Click here to reset your password.</a>";
            body += "<br /><br/><span style='color: #2196F3; font-weight: 100; font-size: 3em;'>Your Snowwhite-Team</span>";

            using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailAddress"], userForMail.Email))
            {
                MailAddress sender = new MailAddress(ConfigurationManager.AppSettings["EmailAddress"], "Snowwhite-Team", Encoding.UTF8);
                mm.Subject = "Resetting for " + userForMail.FirstName + " " + userForMail.LastName;

                mm.Body = body;
                mm.BodyEncoding = Encoding.UTF8;
                mm.IsBodyHtml = true;

                smtpClient.SendAsync(mm, userForMail.Id);
            }
            return Json(await Task.FromResult(0), JsonRequestBehavior.AllowGet);
        }

        // show reset or forgot password form
        public ActionResult ResetPassword(string id)
        {
            return View();
        }

        // this manager handels the authentication mecanism
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // set flag that a user wants to record
        public JsonResult SetDesireRecord()
        {
            this.desireRecord = !this.desireRecord;
            return Json(this.desireRecord ? "ready" : "stopped", JsonRequestBehavior.AllowGet);
        }

        // returns the user object as json object if a user wants to record on the mirror
        [AllowAnonymous]
        public JsonResult DesireRecording()
        {
            if (this.desireRecord)
            {
                SetDesireRecord();
                return Json(this.loggedInUser, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        // saves the uploaded picture to ~/Content/Images/ProfilePictures
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
                        // determining file name. You can format it as you wish.
                        string FileName = profileImage.FileName;
                        // determining file size.
                        int FileSize = profileImage.ContentLength;
                        // creating a byte array corresponding to file size.
                        byte[] FileByteArray = new byte[FileSize];
                        // posted file is being pushed into byte array.
                        profileImage.InputStream.Read(FileByteArray, 0, FileSize);
                        // uploading properly formatted file to server.
                        profileImage.SaveAs(targetLocation + FileName);

                        return FileName;
                    }
                }
                catch (Exception e)
                {
                    AddErrors(new IdentityResult(e.Message));
                    return "";
                }
            }
            return "";
        }

        // error conllection
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}