using Microsoft.AspNetCore.Mvc;
using SouQna.Business.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace SouQna.Presentation.Handlers
{
    public class InvalidOrderStateExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not InvalidOrderStateException stateException)
                return false;

            var problemDetails = new ProblemDetails
            {
                Title = "Invalid Order State",
                Status = StatusCodes.Status400BadRequest,
                Detail = stateException.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}