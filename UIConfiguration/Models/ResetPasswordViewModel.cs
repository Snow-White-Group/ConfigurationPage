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
        [DataType(DataType.Password)]
        public string ActualPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The {0} and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}