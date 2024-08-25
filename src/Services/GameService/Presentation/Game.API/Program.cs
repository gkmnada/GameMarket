using Game.Application.Common.Extensions;
using Game.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Game.API.Registration;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Game.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServer"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        NameClaimType = "username",
    };
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<GameContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMassTransit(options =>
{
    options.AddEntityFrameworkOutbox<GameContext>(x =>
    {
        x.QueryDelay = TimeSpan.FromSeconds(10);
        x.UseSqlServer();
        x.UseBusOutbox();
    });

    options.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("game", false));
    options.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMQ:Host"], "/", host =>
        {
            host.Username(builder.Configuration["RabbitMQ:Username"] ?? "guest");
            host.Password(builder.Configuration["RabbitMQ:Password"] ?? "guest");
        });
        config.ConfigureEndpoints(context);
    });
});

builder.Services.ApplicationService(builder.Configuration);
builder.Services.ApiService(builder.Configuration);

builder.Services.AddGrpc();

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
app.MapGrpcService<GrpcGameService>();

app.Run();
