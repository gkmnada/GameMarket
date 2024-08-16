using Filter.API.Consumers.Game;
using Filter.API.Extensions;
using Filter.API.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddElasticSearch(builder.Configuration);

builder.Services.AddScoped<IFilterGameService, FilterGameService>();

builder.Services.AddMassTransit(options =>
{
    options.AddConsumersFromNamespaceContaining<GameCreatedFilterConsumer>();

    options.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("filter", false));
    options.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMQ:Host"], "/", host =>
        {
            host.Username(builder.Configuration["RabbitMQ:Username"] ?? "guest");
            host.Password(builder.Configuration["RabbitMQ:Password"] ?? "guest");
        });

        config.ReceiveEndpoint("filter-game-created", e =>
        {
            e.ConfigureConsumer<GameCreatedFilterConsumer>(context);
        });

        config.ConfigureEndpoints(context);
    });
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
