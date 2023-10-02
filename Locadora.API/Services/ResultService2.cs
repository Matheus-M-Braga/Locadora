#pragma warning disable CS8618
using FluentValidation.Results;

namespace Locadora.API.Services
{
    public class ResultService2<T>
    {
        public class ResultValidation<T>
        {
            public bool IsSuccess { get; set; } = false;
            public string Message { get; set; } = "Erros de Validação";
            public ICollection<ErrorValidation> Errors { get; set; }

            public static ResultValidation<T> RequestError(ValidationResult validationResult)
            {
                return new ResultValidation<T>
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(x => new ErrorValidation { Message = x.ErrorMessage }).ToList()
                };
            }
        }

        public class FailResponse<T>
        {
            public bool IsSuccess { get; set; } = false;
            public string Message { get; set; }

            public static FailResponse<T> Fail(string message)
            {
                return new FailResponse<T>
                {
                    IsSuccess = false,
                    Message = message
                };
            }
        }

        public class OkResponse
        {
            public bool IsSuccess { get; set; } = true;
            public string Message { get; set; }

            public static OkResponse Oka(string message)
            {
                return new OkResponse
                {
                    IsSuccess = true,
                    Message = message
                };
            }
        }

        public class OkResponse<T> : OkResponse
        {
            public T Response { get; set; }

            public static OkResponse<T> Ok(T data)
            {
                return new OkResponse<T>
                {
                    IsSuccess = true,
                    Response = data
                };
            }
        }

        public class ErrorValidation
        {
            public string Message { get; set; }
        }
    }
}
