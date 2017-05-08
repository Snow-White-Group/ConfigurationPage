using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UIConfiguration.Models
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}