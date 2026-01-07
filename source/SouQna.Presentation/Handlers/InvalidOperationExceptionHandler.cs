using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace SouQna.Presentation.Handlers
{
    public class InvalidOperationExceptionHandler(
        ILogger<InvalidOperationExceptionHandler> logger
    ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not InvalidOperationException invalidOperationException)
                return false;

            logger.LogWarning(
                invalidOperationException,
                "Invalid operation attempted: {Message}",
                invalidOperationException.Message
            );

            var problemDetails = new ProblemDetails
            {
                Title = "Invalid Operation",
                Status = StatusCodes.Status400BadRequest,
                Detail = invalidOperationException.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}