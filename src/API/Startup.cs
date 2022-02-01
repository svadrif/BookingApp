namespace API
{
        public static class Startup
        {
            public static WebApplication InitializeApp(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);
                ConfigureService(builder);
                var app = builder.Build();
                Configure(app);
                return app;
            }

            private static void ConfigureService(WebApplicationBuilder builder)
            {
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }

            private static void Configure(WebApplication app)
            {
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();
            }
        }  
}
