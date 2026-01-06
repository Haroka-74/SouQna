using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace SouQna.Presentation.Handlers
{
    public class ArgumentExceptionHandler(
        ILogger<ArgumentExceptionHandler> logger
    ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not ArgumentException argumentException)
                return false;

            logger.LogWarning(
                argumentException,
                "Invalid argument: {Message}",
                argumentException.Message
            );

            var problemDetails = new ProblemDetails
            {
                Title = "Invalid Argument",
                Status = StatusCodes.Status400BadRequest,
                Detail = argumentException.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            problemDetails.Extensions["parameterName"] = argumentException.ParamName;

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}