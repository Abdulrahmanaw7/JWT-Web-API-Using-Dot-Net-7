using System.ComponentModel.DataAnnotations;

namespace JWTDemo.Model
{
    /* The `public class RegisterModel` is defining a model class in C#. This class is used to represent
    the data structure for registering a user. It contains properties for the user's first name, last
    name, username, email, and password. Each property is decorated with attributes such as
    `[Required]` and `[MaxLength]` to specify validation rules for the data. */
    public class RegisterModel
    {
        /* The `[Required]` attribute is used to specify that the `FirstName` property is required and must
        have a value. If the value is not provided, the validation will fail. */
        [Required, MaxLength(100)]
        /* The code `public string FirstName { get; set; }` is defining a public property called `FirstName`
        of type `string` in the `RegisterModel` class. This property allows getting and setting the value
        of the `FirstName` property. */
        public string FirstName { get; set; }
        [Required, MaxLength(100)]

        /* The code `public string LastName { get; set; } [Required, MaxLength(100)]` is defining a public
        property called `LastName` of type `string` in the `RegisterModel` class. */
        public string LastName { get; set; }
        [Required, MaxLength(100)]

        /* The code `public string UserName { get; set; } [Required, MaxLength(100)]` is defining a public
        property called `UserName` of type `string` in the `RegisterModel` class. */
        public string UserName { get; set; }
        [Required, MaxLength(100)]
        /* The code `public string Email { get; set; } [Required, MaxLength(100)]` is defining a public
        property called `Email` of type `string` in the `RegisterModel` class. */
        public string Email { get; set; }
        [Required, MaxLength(100)]
        /* The line `public string password { get; set; }` is defining a public property called
        `password` of type `string` in the `RegisterModel` class. This property allows getting and
        setting the value of the `password` property. */
        public string password { get; set; }
    }
}
