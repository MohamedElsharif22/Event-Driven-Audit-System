using DH.EventDrivenAuditSystem.APIs.Responses;
using DH.EventDrivenAuditSystem.APIs.Responses.Errors;
using FluentValidation;

namespace DH.EventDrivenAuditSystem.APIs.Middleware
{
    public class ExceptionMiddleware(RequestDelegate? next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IWebHostEnvironment _env = env;


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ValidationException vex)
            {
                _logger.LogWarning(vex, "Validation exception occurred");

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errors = vex.Errors.Select(e => e.ErrorMessage).ToList();
                var response = new ApiValidationErrorResponse { Errors = errors };

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _env.IsDevelopment()
                    ? new ApiExceptionResponse(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace?.ToString())
                    : new ApiResponse(StatusCodes.Status500InternalServerError);

                await context.Response.WriteAsJsonAsync(response);
            }
        }

    }
}
