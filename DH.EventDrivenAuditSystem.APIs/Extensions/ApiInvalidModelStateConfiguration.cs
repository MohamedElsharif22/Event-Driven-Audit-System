using DH.EventDrivenAuditSystem.APIs.Responses.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DH.EventDrivenAuditSystem.APIs.Extensions
{
    public static class ApiInvalidModelStateConfiguration
    {
        public static IServiceCollection AddApiInvalidModelStateConfiguration(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
