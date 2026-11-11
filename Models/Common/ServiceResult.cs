using System.Text.Json.Serialization;

namespace APPC.Models.Common
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; init; }

        // [JsonIgnore] // Used by Controller for HTTP status, hidden from JSON body
        public int StatusCode { get; init; }
        public string Message { get; init; } = string.Empty;
        public T? Data { get; init; }

        public static ServiceResult<T> Ok(T data, string message, int statusCode = 200)
            => new() { IsSuccess = true, StatusCode = statusCode, Message = message, Data = data };

        public static ServiceResult<T> Fail(int statusCode, string message)
            => new() { IsSuccess = false, StatusCode = statusCode, Message = message };
    }
}
