using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace SouQna.Presentation.Handlers
{
    public class EmailNotConfirmedExceptionHandler(
        ILogger<EmailNotConfirmedExceptionHandler> logger
    ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not EmailNotConfirmedException emailNotConfirmedException)
                return false;

            logger.LogWarning(
                emailNotConfirmedException,
                "Email not confirmed: {Message}",
                emailNotConfirmedException.Message
            );

            var problemDetails = new ProblemDetails
            {
                Title = "Email Not Confirmed",
                Status = StatusCodes.Status403Forbidden,
                Detail = emailNotConfirmedException.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}