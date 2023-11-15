using System.ComponentModel.DataAnnotations;

namespace JWTDemo.Model
{
    public class TokenRequestModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
