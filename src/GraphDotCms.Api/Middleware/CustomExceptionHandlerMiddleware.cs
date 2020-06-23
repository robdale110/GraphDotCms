using GraphDotCms.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text.Json;
using System.Threading.Tasks;

namespace GraphDotCms.Api.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IHostEnvironment environment)
        {
            try
            {
                await _next(context);

                switch (context.Response.StatusCode)
                {
                    case 401:
                        await WriteResponse(context,
                            GenerateProblemDetails(context, "Unauthorized", context.Response.StatusCode));
                        break;
                    case 404:
                        await WriteResponse(context, GenerateProblemDetails(context, "Not Found", context.Response.StatusCode));
                        break;
                }
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception, environment);
            }
        }

        private static async Task WriteResponse(HttpContext context, ProblemDetails problemDetails)
        {
            context.Response.ContentType = "application/problem+json";
            var stream = context.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problemDetails);
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, IHostEnvironment environment)
        {
            var title = string.Empty;
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var details = environment.IsDevelopment() ? exception.ToString() : null;

            if (exception is RestException restException)
            {
                title = restException.Title;
                statusCode = (int)restException.StatusCode;
            }

            await WriteResponse(context, GenerateProblemDetails(context, title, statusCode, details));
        }

        private static ProblemDetails GenerateProblemDetails(HttpContext context, string title, int statusCode,
            string details = null)
        {
            var problem = new ProblemDetails
            {
                Type = $"https://httpstatuses.com/{statusCode}",
                Status = statusCode,
                Title = title,
                Detail = details,
            };
            var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;

            if (traceId != null)
            {
                problem.Extensions["traceId"] = traceId;
            }

            return problem;
        }
    }
}
