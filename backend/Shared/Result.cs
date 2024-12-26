namespace Shared
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        protected Result(bool isSuccess, string message, IEnumerable<string> errors)
        {
            IsSuccess = isSuccess;
            Message = message;
            Errors = errors ?? Enumerable.Empty<string>();
        }

        public static Result Success(string message = "Success!")
        {
            return new Result(true, message, null);
        }

        public static Result Failure(params string[] errors)
        {
            return new Result(false, null, errors);
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, null, errors);
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; private set; }

        private Result(bool isSuccess, string message, T data, IEnumerable<string> errors)
            : base(isSuccess, message, errors)
        {
            Data = data;
        }

        public static Result<T> Success(T data, string message = "Success")
        {
            return new Result<T>(true, message, data, null);
        }

        public static new Result<T> Failure(params string[] errors)
        {
            return new Result<T>(false, null, default, errors);
        }

        public static new Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>(false, null, default, errors);
        }
    }
}
