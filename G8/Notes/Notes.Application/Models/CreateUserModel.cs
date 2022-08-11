﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Models
{
    public class CreateUserModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]

        public string LastName { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Compare("ConfirmPassword", ErrorMessage = "Password doesn't match")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
