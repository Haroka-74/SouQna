using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace SouQna.Presentation.Handlers
{
    public class InvalidRefreshTokenExceptionHandler(
        ILogger<InvalidRefreshTokenExceptionHandler> logger
    ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not InvalidRefreshTokenException invalidRefreshTokenException)
                return false;

            logger.LogWarning(
                invalidRefreshTokenException,
                "Invalid refresh token: {Message}",
                invalidRefreshTokenException.Message
            );

            var problemDetails = new ProblemDetails
            {
                Title = "Invalid Refresh Token",
                Status = StatusCodes.Status401Unauthorized,
                Detail = invalidRefreshTokenException.Message,
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}