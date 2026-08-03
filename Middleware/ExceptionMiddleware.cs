using System.Net;
using System.Text.Json;
using Serilog;

namespace EmployeeLeaveManagement.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            
{
    // Log the exception
    Log.Error(ex,
        "Unhandled Exception. Request: {Method} {Path}",
        context.Request.Method,
        context.Request.Path);

    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

    var response = new
    {
        StatusCode = context.Response.StatusCode,
        Message = "An unexpected error occurred.",
        Details = ex.InnerException?.Message ?? ex.Message
    };

    await context.Response.WriteAsync(
        JsonSerializer.Serialize(response)
    );
}
        }
    }
}