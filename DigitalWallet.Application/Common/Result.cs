using System.Net;

namespace DigitalWallet.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public List<string> Errors { get; } = new();
        public HttpStatusCode StatusCode { get; }

        protected Result(HttpStatusCode statusCode)
        {
            IsSuccess = true;
            StatusCode = statusCode;
        }

        protected Result(List<string> errors, HttpStatusCode statusCode)
        {
            IsSuccess = false;
            Errors = errors;
            StatusCode = statusCode;
        }

        protected Result(string error, HttpStatusCode statusCode)
        {
            IsSuccess = false;
            Errors = new List<string> { error };
            StatusCode = statusCode;
        }

        public static Result Success(HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(statusCode);

        public static Result Failure(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(errors, statusCode);

        public static Result Failure(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(error, statusCode);
    }

    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Data { get; }
        public List<string> Errors { get; } = new();
        public HttpStatusCode StatusCode { get; }

        private Result(T data, HttpStatusCode statusCode)
        {
            IsSuccess = true;
            Data = data;
            StatusCode = statusCode;
        }

        private Result(List<string> errors, HttpStatusCode statusCode)
        {
            IsSuccess = false;
            Errors = errors;
            StatusCode = statusCode;
            Data = default;
        }

        private Result(string error, HttpStatusCode statusCode)
        {
            IsSuccess = false;
            Errors = new List<string> { error };
            StatusCode = statusCode;
            Data = default;
        }

        public static Result<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(data, statusCode);

        public static Result<T> Failure(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(errors, statusCode);

        public static Result<T> Failure(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(error, statusCode);
    }
}
