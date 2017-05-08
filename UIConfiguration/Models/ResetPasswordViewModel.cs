using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UIConfiguration.Models
{
    public class ResetPasswordViewModel
    {
        [Display(Name="Actual Password")]
        public string ActualPassword { get; set; }
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Display(Name = "New Password Confirm")]
        public string ConfirmNewPassword { get; set; }
    }
}