using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UIConfiguration.Models
{
    public class SnowwhiteUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Thumbnail { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public string SpeechID { get; set; }
    }
}