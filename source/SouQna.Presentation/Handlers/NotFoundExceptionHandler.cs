using Microsoft.AspNetCore.Mvc;
using SouQna.Business.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace SouQna.Presentation.Handlers
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not NotFoundException notFoundException)
                return false;

            var problemDetails = new ProblemDetails
            {
                Title = "Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = notFoundException.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}