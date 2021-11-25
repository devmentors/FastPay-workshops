using System;
using System.Collections.Concurrent;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FastPay.Domain.Exceptions;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FastPay.Infrastructure.Exceptions
{
    internal sealed class ErrorHandlerMiddleware : IMiddleware
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        private static readonly ConcurrentDictionary<Type, string> Codes = new();
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleErrorAsync(context, exception);
            }
        }

        private static async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var (error, code) = Map(exception);
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(JsonSerializer.Serialize(error, SerializerOptions));
        }

        private static (Error error, HttpStatusCode code) Map(Exception exception)
            => exception switch
            {
                FastPayException ex => (new Error(GetErrorCode(ex), ex.Message), HttpStatusCode.BadRequest),
                _ => (new Error("error", "There was an error."), HttpStatusCode.InternalServerError)
            };

        private record Error(string Code, string Message);

        private static string GetErrorCode(Exception exception)
        {
            var type = exception.GetType();
            return Codes.GetOrAdd(type, type.Name.Replace("Exception", string.Empty).Underscore());
        }
    }
}