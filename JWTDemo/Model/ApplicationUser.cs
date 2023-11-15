using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JWTDemo.Model
{
   /* The class ApplicationUser represents a user in an application and includes properties for their
   first name and last name. */
    public class ApplicationUser:IdentityUser
    {
     /* The `[Required]` attribute is used to specify that the `FirstName` property is required and
     must have a value. If a value is not provided for this property, a validation error will occur. */
        [Required,MaxLength(70)]
        public string FirstName { get; set; }
      /* The `[Required]` attribute is used to specify that the `LastName` property is required and
      must have a value. If a value is not provided for this property, a validation error will
      occur. */
        [Required, MaxLength(70)]
        public string LastName { get; set; }      
    }

}
