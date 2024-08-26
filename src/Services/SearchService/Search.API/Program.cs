using MassTransit;
using Search.API.Consumers.Game;
using Search.API.Data;
using Search.API.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(options =>
{
    options.AddConsumersFromNamespaceContaining<GameCreatedConsumer>();

    options.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));
    options.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMQ"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        config.ReceiveEndpoint("search-game-created", e =>
        {
            e.ConfigureConsumer<GameCreatedConsumer>(context);
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

app.Lifetime.ApplicationStarted.Register(async () =>
{
    try
    {
        await DatabaseInitializer.InitializeAsync(app);
    }
    catch (Exception)
    {
        throw;
    }

});

app.Run();
