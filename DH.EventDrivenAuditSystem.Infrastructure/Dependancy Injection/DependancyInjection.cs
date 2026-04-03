using DH.EventDrivenAuditSystem.Application.Interfaces;
using DH.EventDrivenAuditSystem.Domain.Common;
using DH.EventDrivenAuditSystem.Infrastructure.BackgroundServices;
using DH.EventDrivenAuditSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.EventDrivenAuditSystem.Infrastructure.Dependancy_Injection
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
            // Register repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register background services
            services.AddSingleton<AuditChannel>();
            services.AddSingleton<IAuditQueue>(sp => sp.GetRequiredService<AuditChannel>());
            services.AddHostedService<AuditWorker>();

            return services;
        }

    }
}
