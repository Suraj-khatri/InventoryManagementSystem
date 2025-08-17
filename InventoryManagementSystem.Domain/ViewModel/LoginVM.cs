using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
  public  class LoginVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnURL { get; set; }
        public bool IsRemember { get; set; }
    }
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
    public class ChangePasswordVM
    {
        public int? AuthId { get; set; }
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        //[RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,}$"
        //    , ErrorMessage = @"Password should contain 1 capital letter, 1 special character
        //, 1 numeric digit and at least 6 characters")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Should be same as new password.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
