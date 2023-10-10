using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace Locadora.API.Services {
    public class ResultService {
        public bool IsSucess { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<ErrorValidation>? Errors { get; init; }

        public static ResultService RequestError(string message, ValidationResult validationResult) {
            return new ResultService {
                IsSucess = false,
                Message = message,
                Errors = validationResult.Errors.Select(x => new ErrorValidation { Message = x.ErrorMessage, Field = x.PropertyName }).ToArray(),
            };
        }

        public static ResultService<T> RequestError<T>(string message, ValidationResult validationResult) {
            return new ResultService<T> {
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

    public class ResultService<T> : ResultService {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Response { get; set; }
    }

    public class ErrorValidation {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Field { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

    }
}