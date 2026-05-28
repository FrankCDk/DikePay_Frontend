namespace DikePay.Application.DTOs.Api
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Code { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public T Data { get; init; } = default!;
        public List<ApiError> Errors { get; init; } = [];
        public string TraceId { get; init; } = string.Empty;

        public static ApiResponse<T> Ok(T data, string message = "Operación realizada con éxito", string code = "SUCCESS")
            => new()
            {
                Success = true,
                Code = code,
                Message = message,
                Data = data
            };

        public static ApiResponse<T> Ok(string message = "Operación realizada con éxito", string code = "SUCCESS")
            => new()
            {
                Success = true,
                Code = code,
                Message = message,
                Data = default
            };

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
