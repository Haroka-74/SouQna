using Microsoft.AspNetCore.Mvc;
using SouQna.Business.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace SouQna.Presentation.Handlers
{
    public class ConflictExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not ConflictException conflictException)
                return false;

            var problemDetails = new ProblemDetails
            {
                Title = "Conflict",
                Status = StatusCodes.Status409Conflict,
                Detail = conflictException.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8"
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}