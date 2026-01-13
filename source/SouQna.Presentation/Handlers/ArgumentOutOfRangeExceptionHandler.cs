using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace SouQna.Presentation.Handlers
{
    public class ArgumentOutOfRangeExceptionHandler(
        ILogger<ArgumentOutOfRangeExceptionHandler> logger
    ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not ArgumentOutOfRangeException argumentOutOfRangeException)
                return false;

            logger.LogWarning(
                argumentOutOfRangeException,
                "Argument out of range: {ParamName} - {Message}",
                argumentOutOfRangeException.ParamName,
                argumentOutOfRangeException.Message
            );

            var problemDetails = new ProblemDetails
            {
                Title = "Invalid Value",
                Status = StatusCodes.Status400BadRequest,
                Detail = argumentOutOfRangeException.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            problemDetails.Extensions["parameterName"] = argumentOutOfRangeException.ParamName;

            if (argumentOutOfRangeException.ActualValue != null)
            {
                problemDetails.Extensions["actualValue"] = argumentOutOfRangeException.ActualValue.ToString();
            }

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}