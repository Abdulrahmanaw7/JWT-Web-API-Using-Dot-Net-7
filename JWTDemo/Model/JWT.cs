namespace JWTDemo.Model
{
    /* The `public class JWT` is defining a class named `JWT` in the `JWTDemo.Model` namespace. This
    class has four properties: `Key`, `Issuer`, `Audience`, and `DurationInDays`. These properties
    define the key, issuer, audience, and duration of a JSON Web Token (JWT) respectively. */
    public class JWT
    {
        /* The line `public string Key { get; set; }` is defining a public property named `Key` of type
        `string` in the `JWT` class. This property has both a getter and a setter, which allows other
        parts of the code to read and modify the value of the `Key` property. */
        public string Key { get; set; }
        /* The line `public string Issuer { get; set; }` is defining a public property named `Issuer` of type
        `string` in the `JWT` class. This property has both a getter and a setter, which allows other
        parts of the code to read and modify the value of the `Issuer` property. */
        public string Issuer { get; set; }
        /* The line `public string Audience { get; set; }` is defining a public property named `Audience` of
        type `string` in the `JWT` class. This property has both a getter and a setter, which allows other
        parts of the code to read and modify the value of the `Audience` property. */
        public string Audience { get; set; }
        /* The line `public double DurationInDays { get; set; }` is defining a public property named
        `DurationInDays` of type `double` in the `JWT` class. This property has both a getter and a
        setter, which allows other parts of the code to read and modify the value of the `DurationInDays`
        property. It represents the duration (in days) for which the JSON Web Token (JWT) is valid. */
        public double DurationInDays { get; set; }
    }
}
