using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServer"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        NameClaimType = "username",
    };
});

app.MapGet("/", () => "Hello World!");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
