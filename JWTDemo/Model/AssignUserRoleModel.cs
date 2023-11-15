namespace JWTDemo.Model
{
    /* The AssignUserRoleModel class represents a model for assigning a role to a user. */
    public class AssignUserRoleModel
    {
        /* The line `public string UserId { get; set; }` is declaring a public property called `UserId` of
        type `string` in the `AssignUserRoleModel` class. This property has both a getter and a setter,
        allowing you to get and set the value of the `UserId` property. */
        public string UserId { get; set; }/* The line `public string Role { get; set; }` is declaring
        a public property called `Role` of type `string` in the
        `AssignUserRoleModel` class. This property has both a
        getter and a setter, allowing you to get and set the
        value of the `Role` property. */

        public string Role { get; set; }
    }
}
