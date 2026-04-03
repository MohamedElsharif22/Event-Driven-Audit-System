
using DH.EventDrivenAuditSystem.APIs.Extensions;
using DH.EventDrivenAuditSystem.APIs.Middleware;
using DH.EventDrivenAuditSystem.Application.Dependancy_Injection;
using DH.EventDrivenAuditSystem.Infrastructure.Data;
using DH.EventDrivenAuditSystem.Infrastructure.Dependancy_Injection;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace DH.EventDrivenAuditSystem.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();

            // Add Layers Services
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            //Api Invalid Model State Configuration
            builder.Services.AddApiInvalidModelStateConfiguration();

            var app = builder.Build();

            #region Run Migration

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _appDbContext = services.GetRequiredService<AppDbContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _appDbContext.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AppDbContext>();

                logger.LogError(ex, "an error has been occured while running migration!");
            }
            #endregion

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();
            //Map Endpoints
            app.MapControllers();

            app.Run();
        }
    }
}
