using SouQna.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace SouQna.Presentation.Handlers
{
    public class InsufficientStockExceptionHandler(
        ILogger<InsufficientStockExceptionHandler> logger
    ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not InsufficientStockException insufficientStockException)
                return false;

            logger.LogWarning(
                insufficientStockException,
                "Insufficient stock: Available={Available}, Requested={Requested}",
                insufficientStockException.Available,
                insufficientStockException.Requested
            );

            var problemDetails = new ProblemDetails
            {
                Title = "Insufficient Stock",
                Status = StatusCodes.Status400BadRequest,
                Detail = insufficientStockException.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}