using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using SouQna.Application.Exceptions.Enums;

namespace SouQna.Presentation.Handlers
{
    public class AuthenticationExceptionHandler(
        ILogger<AuthenticationExceptionHandler> logger
    ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not AuthenticationException authException)
                return false;

            logger.LogWarning(
                authException,
                "Authentication failed: {ErrorType} - {Message}",
                authException.ErrorType,
                authException.Message
            );

            var (statusCode, title, type) = authException.ErrorType switch
            {
                AuthenticationErrorType.EmailNotConfirmed => (
                    StatusCodes.Status403Forbidden,
                    "Access Forbidden",
                    "https://tools.ietf.org/html/rfc7231#section-6.5.3"
                ),

                AuthenticationErrorType.InvalidCredentials => (
                    StatusCodes.Status401Unauthorized,
                    "Authentication Failed",
                    "https://tools.ietf.org/html/rfc7235#section-3.1"
                ),

                AuthenticationErrorType.InvalidRefreshToken => (
                    StatusCodes.Status401Unauthorized,
                    "Invalid Token",
                    "https://tools.ietf.org/html/rfc7235#section-3.1"
                ),

                AuthenticationErrorType.TokenExpired => (
                    StatusCodes.Status401Unauthorized,
                    "Token Expired",
                    "https://tools.ietf.org/html/rfc7235#section-3.1"
                ),

                AuthenticationErrorType.TokenRevoked => (
                    StatusCodes.Status401Unauthorized,
                    "Token Revoked",
                    "https://tools.ietf.org/html/rfc7235#section-3.1"
                ),

                _ => (
                    StatusCodes.Status401Unauthorized,
                    "Authentication Failed",
                    "https://tools.ietf.org/html/rfc7235#section-3.1"
                )
            };

            var problemDetails = new ProblemDetails
            {
                Title = title,
                Status = statusCode,
                Detail = authException.Message,
                Type = type
            };

            problemDetails.Extensions["errorType"] = authException.ErrorType.ToString();

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}