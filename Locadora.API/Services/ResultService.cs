using System.Net;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;

namespace Locadora.API.Services
{
    public class ResultService : IFilterMetadata
    {
        public bool IsSucess { get; set; }
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<ErrorValidation>? Errors { get; init; }

        public static ResultService RequestError(string message, ValidationResult validationResult)
        {
            return new ResultService
            {
                IsSucess = false,
                Message = message,
                Errors = validationResult.Errors.Select(x => new ErrorValidation { Message = x.ErrorMessage, Field = x.PropertyName }).ToArray(),
            };
        }

        public static ResultService<T> RequestError<T>(string message, ValidationResult validationResult)
        {
            return new ResultService<T>
            {
                IsSucess = false,
                Message = message,
                Errors = validationResult.Errors.Select(x => new ErrorValidation { Message = x.ErrorMessage, Field = x.PropertyName }).ToArray(),
            };
        }

        public static ResultService Fail(string message) => new() { IsSucess = false, Message = message };
        public static ResultService<T> Fail<T>(string message) => new() { IsSucess = false, Message = message };

        public static ResultService Ok(string message) => new() { IsSucess = true, Message = message };
        public static ResultService<T> Ok<T>(T data) => new() { IsSucess = true, Response = data };
    }

    public class ResultService<T> : ResultService
    {
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Response { get; set; }
    }

    public class ErrorValidation
    {
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Field { get; set; }
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

    }

    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Lidar com a exce��o aqui (por exemplo, registrar a exce��o)

                // Configurar a resposta de erro
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new ErrorResponse
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "buceta server error",
                    Status = context.Response.StatusCode,
                    Errors = new Dictionary<string, string[]>
                {
                    { "Error", new[] { "An unexpected error occurred." } }
                }
                };

                var jsonResponse = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }

    public class ErrorResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }

}