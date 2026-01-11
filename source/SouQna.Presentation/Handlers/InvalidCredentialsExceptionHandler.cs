using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace SouQna.Presentation.Handlers
{
    public class InvalidCredentialsExceptionHandler(
        ILogger<InvalidCredentialsExceptionHandler> logger
    ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not InvalidCredentialsException invalidCredentialsException)
                return false;

            logger.LogWarning(
                invalidCredentialsException,
                "Authentication failed: {Message}",
                invalidCredentialsException.Message
            );

            var problemDetails = new ProblemDetails
            {
                Title = "Authentication Failed",
                Status = StatusCodes.Status401Unauthorized,
                Detail = invalidCredentialsException.Message,
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}