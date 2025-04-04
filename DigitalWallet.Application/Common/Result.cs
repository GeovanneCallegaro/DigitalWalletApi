using System.Net;

namespace DigitalWallet.Application.Common
{
    public abstract class Result
    {
        protected Result(bool isSuccess, HttpStatusCode statusCode, List<string>? message = null)
        {
            IsSuccess = isSuccess;
            Message = message ?? [];
            HttpStatusCode = statusCode;
        }

        public bool IsSuccess { get; private set; }
        public List<string> Message { get; private set; }
        public HttpStatusCode HttpStatusCode { get; private set; }
    }
}
