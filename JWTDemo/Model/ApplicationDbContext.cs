using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWTDemo.Model
{
  /* The class ApplicationDbContext is a subclass of IdentityDbContext<ApplicationUser> and is used to
  define the application's database context. */
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
    /* The line `public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
    options):base(options) { }` is the constructor of the `ApplicationDbContext` class. It takes an
    instance of `DbContextOptions<ApplicationDbContext>` as a parameter and passes it to the base
    class constructor using the `base(options)` syntax. This allows the `ApplicationDbContext` class
    to inherit the functionality of the `IdentityDbContext<ApplicationUser>` class and initialize it
    with the provided options. */
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
      
    }
}
