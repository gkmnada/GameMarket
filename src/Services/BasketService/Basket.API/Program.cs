using Basket.API.Clients;
using Basket.API.Services;
using Game.Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(options =>
{
    options.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("basket", false));
    options.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMQ"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
        config.ConfigureEndpoints(context);
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServer"];
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        NameClaimType = "username"
    };
});

builder.Services.AddScoped<IBasketService, BasketService>();

builder.Services.AddScoped<GrpcDiscountClient>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
