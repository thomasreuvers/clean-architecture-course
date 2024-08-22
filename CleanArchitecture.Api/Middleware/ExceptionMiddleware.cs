using System.Net;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Application.Exceptions;
using Newtonsoft.Json;

namespace CleanArchitecture.Api.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        CustomValidationProblemDetails problem = new();

        switch (ex)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomValidationProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomValidationProblemDetails
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Detail = notFoundException.InnerException?.Message,
                    Type = nameof(NotFoundException)
                };
                break;
            default:
                problem = new CustomValidationProblemDetails
                {
                    Title = ex.Message,
                    Status = (int)statusCode,
                    Detail = ex.InnerException?.Message,
                    Type = nameof(HttpStatusCode.InternalServerError)
                };
                break;
        }
        
        context.Response.StatusCode = (int)statusCode;
        var logMessage = JsonConvert.SerializeObject(problem);
        logger.LogError(logMessage);
        await context.Response.WriteAsJsonAsync(problem);
    }
}