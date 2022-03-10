using Serilog;
using Serilog.Events;
using Application.Interfaces;
using Telegram.Bot;
using TelegramBot;
using Application.Profiles;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Infrastructure.Repositories;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .CreateBootstrapLogger();


var builder = WebApplication.CreateBuilder(args);

// Reading AppSettings + Enebles Serilog
// Full setup of serilog. We read log settings from appsettings.json
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1",new OpenApiInfo {Title = "BookingApp API", Version = "v1" });
        c.CustomSchemaIds(type => type.ToString());
    });
builder.Services.AddAutoMapper(typeof(VacationProfile));
builder.Services.AddAutoMapper(typeof(WorkPlaceProfile));


// Dependency injections
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddTelegramBot();

builder.Services.AddHttpClient("tgwebhook")
                .AddTypedClient<ITelegramBotClient>(httpClient
                    => new TelegramBotClient(builder.Configuration["BotToken"], httpClient));

var app = builder.Build();

// Logging every request
// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging(configure =>
{
    configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
}); // We want to log all HTTP requests

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}
    app.UseHttpsRedirection();

app.UseRouting();

    app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "tgwebhook",
                                 pattern: $"api/TelegramBot",
                                 new { controller = "TelegramBot", action = "Post" });
    endpoints.MapControllers();
});

    app.Run();
