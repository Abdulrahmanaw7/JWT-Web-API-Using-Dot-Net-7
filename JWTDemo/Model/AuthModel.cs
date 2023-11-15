namespace JWTDemo.Model
{
    /// <summary>
    /// AuthModel To map the Result from JWT
    /// </summary>
    public class AuthModel
    {
        /* The line `public string Message { get; set;}` is declaring a public property called "Message" of
        type string in the AuthModel class. This property allows getting and setting the value of the
        "Message" field. */
        public string Message { get; set; }
        /* The line `public bool IsAuthenticated { get; set; }` is declaring a public property called
        "IsAuthenticated" of type bool in the AuthModel class. This property allows getting and setting the
        value of the "IsAuthenticated" field. It is used to indicate whether the user is authenticated or
        not. */
        public bool IsAuthenticated { get; set; }
        /* The line `public string Username { get; set;}` is declaring a public property called "Username" of
        type string in the AuthModel class. This property allows getting and setting the value of the
        "Username" field. It is used to store the username of the authenticated user. */
        public string Username { get; set; }
        /* The line `public string Email { get; set;}` is declaring a public property called "Email" of type
        string in the AuthModel class. This property allows getting and setting the value of the "Email"
        field. It is used to store the email address of the authenticated user. */
        public string Email { get; set; }
        /* The line `public List<string> Roles{ get; set;}` is declaring a public property called "Roles" of
        type List<string> in the AuthModel class. This property allows getting and setting the value of the
        "Roles" field. It is used to store the roles or permissions associated with the authenticated user.
        The List<string> type indicates that the Roles property can hold multiple string values. */
        public List<string> Roles { get; set; }
        /* The line `public string Token { get; set; }` is declaring a public property called "Token" of type
        string in the AuthModel class. This property allows getting and setting the value of the "Token"
        field. It is used to store the JWT (JSON Web Token) generated for the authenticated user. The JWT is
        a secure way to transmit information between parties as a JSON object. */
        public string Token { get; set; }
        /* The line `public DateTime ExpiresOn { get; set; }` is declaring a public property called "ExpiresOn"
        of type DateTime in the AuthModel class. This property allows getting and setting the value of the
        "ExpiresOn" field. It is used to store the expiration date and time of the JWT (JSON Web Token)
        generated for the authenticated user. The ExpiresOn property indicates when the JWT will expire and
        become invalid. */
        public DateTime ExpiresOn { get; set; }

    }
}
