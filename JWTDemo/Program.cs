using JWTDemo.Model;
using JWTDemo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
Microsoft.Extensions.Configuration.ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
/* `builder.Services.AddIdentity<ApplicationUser,
IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();` is configuring the identity
system in the application. */
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
/* The line `builder.Services.Configure<JWT>(configuration.GetSection("JWT"));` is configuring the JWT
(JSON Web Token) settings in the application. It is binding the values from the "JWT" section of the
configuration file to an instance of the `JWT` class. This allows the application to access the JWT
settings through dependency injection. */

builder.Services.Configure<JWT>(configuration.GetSection("JWT"));

/* The line `builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));` is configuring the
application's database context. */
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/* `builder.Services.AddEndpointsApiExplorer();` is adding the API Explorer services to the
application's service container. The API Explorer services are used to generate OpenAPI/Swagger
documentation for the application's endpoints. This allows developers to easily explore and
understand the available API endpoints and their associated request/response models. */
builder.Services.AddEndpointsApiExplorer();
/* `builder.Services.AddSwaggerGen()` is adding the Swagger generation services to the application's
service container. Swagger is a tool that helps generate interactive API documentation. */
builder.Services.AddSwaggerGen();

 
/* The code `builder.Services.AddScoped<IAuthService, AuthService>();` is registering the `AuthService`
class as a scoped service in the application's service container. This means that a new instance of
`AuthService` will be created for each HTTP request and will be available for dependency injection. */
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(
    o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = configuration["JWT:Audience"],
            ValidIssuer = configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]))
        };
    }
    );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//UseAuthentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
