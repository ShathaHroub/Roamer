﻿using System.ComponentModel.DataAnnotations;

namespace Roamer.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email field cannot be empty")]
        [EmailAddress(ErrorMessage = "Example : user@gmail.com")]
        [MaxLength(50, ErrorMessage = "Maximum 50 character for email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password field cannot be empty")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
