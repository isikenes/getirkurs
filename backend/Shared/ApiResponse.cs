namespace Shared
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public ApiResponse(T data, bool success, string message)
        {
            Data = data;
            Success = success;
            Message = message;
        }
    }
}
