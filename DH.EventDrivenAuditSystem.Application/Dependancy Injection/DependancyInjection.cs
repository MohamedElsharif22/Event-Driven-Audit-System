using DH.EventDrivenAuditSystem.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DH.EventDrivenAuditSystem.Application.Dependancy_Injection
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register MediatR
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            // Register FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // Register pipeline behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
