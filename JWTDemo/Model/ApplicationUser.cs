﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JWTDemo.Model
{
    public class ApplicationUser:IdentityUser
    {
        [Required,MaxLength(70)]
        public string FirstName { get; set; }
        [Required, MaxLength(70)]
        public string LastName { get; set; }      
    }

}
