using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServer"];
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        NameClaimType = "username",
    };
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapReverseProxy();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
