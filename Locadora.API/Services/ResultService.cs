using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace Locadora.API.Services
{
    public class ResultService
    {
        public bool IsSucess { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[]? Errors { get; init; }

        public static ResultService RequestError(ValidationResult validationResult)
        {
            return new ResultService
            {
                IsSucess = false,
                Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray(),
            };
        }

        public static ResultService Fail(string message) => new() { IsSucess = false, Message = message };
        public static ResultService<T> Fail<T>(string message) => new() { IsSucess = false, Message = message };

        public static ResultService Ok(string message) => new() { IsSucess = true, Message = message };
        public static ResultService<T> Ok<T>(T data)
        {
            return new ResultService<T>
            {
                Response = data,
                IsSucess = true
            };
        }
    }

    public class ResultService<T> : ResultService
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Response { get; set; }
    }

    
}