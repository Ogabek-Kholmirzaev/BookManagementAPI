using BookManagement.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.API.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Exception occured");

            (string Detail, string Title, int StatusCode) details = exception switch
            {
                TitleAlreadyExistsException =>
                    (exception.Message, exception.GetType().Name, StatusCodes.Status400BadRequest),
                BookNotFoundException =>
                    (exception.Message, exception.GetType().Name, StatusCodes.Status404NotFound),
                _ =>
                    (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError)
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

            httpContext.Response.StatusCode = details.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
