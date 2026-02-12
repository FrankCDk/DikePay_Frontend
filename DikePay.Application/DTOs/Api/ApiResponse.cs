namespace DikePay.Application.DTOs.Api
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Code { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public T Data { get; init; }
        public List<ApiError> Errors { get; init; } = [];
        public string TraceId { get; init; } = string.Empty;        
        public static ApiResponse<T> Fail(string code, string message, List<ApiError>? errors = null)
            => new()
            {
                Success = false,
                Code = code,
                Message = message,
                Errors = errors ?? []
            };
    }

    public class ApiError
    {
        public string Code { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public string Field { get; init; } = string.Empty;
    }
}
