using System.ComponentModel.DataAnnotations;

namespace JWTDemo.Model
{
    /* The `public class TokenRequestModel` is a model class in C# that represents the data structure
    for a token request. It has two properties: `UserName` and `Password`, both of which are
    required fields. This class is used to validate and store the data entered by the user when
    making a token request. */
    public class TokenRequestModel
    {
        /* The `[Required]` attribute is used to specify that the `UserName` property is a required field.
        This means that when an instance of the `TokenRequestModel` class is created, the `UserName`
        property must have a value assigned to it. If the `UserName` property is not provided with a
        value, a validation error will occur. */
        [Required]
        public string UserName { get; set; }

        /* The `[Required]` attribute is used to specify that the `Password` property is a required
        field. This means that when an instance of the `TokenRequestModel` class is created, the
        `Password` property must have a value assigned to it. If the `Password` property is not
        provided with a value, a validation error will occur. */
        [Required]
        public string Password { get; set; }
    }
}
