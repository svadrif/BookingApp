using Application;
using Application.Authentication;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .CreateBootstrapLogger();

JwtSettings _jwtSettings = new JwtSettings();

var builder = WebApplication.CreateBuilder(args);

// Reading AppSettings + Enebles Serilog
// Full setup of serilog. We read log settings from appsettings.json
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());
builder.Configuration.Bind(_jwtSettings);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookingApp API", Version = "v1" });
    c.CustomSchemaIds(type => type.ToString());
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                },
                Array.Empty<string>()
            },
        });
});

builder.Services.AddAuthorization();

// Dependency injections
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

//builder.Services.AddTelegramBot();

/*builder.Services.AddHttpClient("tgwebhook")
                .AddTypedClient<ITelegramBotClient>(httpClient
                    => new TelegramBotClient(builder.Configuration["BotToken"], httpClient));*/

builder.Services.AddCors(options =>
{
    options.AddPolicy("AnyOrigin", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

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

app.UseAuthentication();

app.UseRouting();

app.UseCors("AnyOrigin");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    /*    endpoints.MapControllerRoute(name: "tgwebhook",
                                     pattern: $"api/TelegramBot",
                                     new { controller = "TelegramBot", action = "Post" });*/
    endpoints.MapControllers();
});

app.Run();
