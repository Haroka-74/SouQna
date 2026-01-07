using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace SouQna.Presentation.Handlers
{
    public class NotFoundExceptionHandler(
        ILogger<NotFoundExceptionHandler> logger
    ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not NotFoundException notFoundException)
                return false;

            logger.LogError(
                notFoundException,
                "Exception occurred: {Message}",
                notFoundException.Message
            );

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