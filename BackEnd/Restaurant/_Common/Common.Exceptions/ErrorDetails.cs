using System.Net;

namespace Common.Exceptions
{
    public class ErrorDetails
    {
        public HttpStatusCode StatusCode { get; private set; }

        public string Message { get; private set; }

        private ErrorDetails(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public static ErrorDetails Create(HttpStatusCode statusCode, string message)
        {
            return new ErrorDetails(statusCode, message);
        }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}