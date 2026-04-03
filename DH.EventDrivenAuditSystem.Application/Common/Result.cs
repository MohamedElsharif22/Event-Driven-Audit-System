namespace DH.EventDrivenAuditSystem.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Value { get; set; }
        public string? ErrorMessage { get; set; }

        public static Result<T> Success(T value)
        {
            return new Result<T> { IsSuccess = true, Value = value };
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T> { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
}
