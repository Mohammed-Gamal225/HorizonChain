namespace Horizon.Application.Abstractions.Common;
public class Result<T>
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public T? Value { get; }
    public ErrorCode Code { get; }

    public bool IsFailure => !IsSuccess;

    private Result(bool isSuccess, T? value, string? error, ErrorCode code)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        Code = code;
    }

    public static Result<T> Success(T value) =>
        new(true, value, null, ErrorCode.None);

    public static Result<T> Failure(string error, ErrorCode code = ErrorCode.Internal) =>
        new(false, default, error, code);
}
public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public ErrorCode Code { get; }

    public bool IsFailure => !IsSuccess;

    private Result(bool isSuccess, string? error, ErrorCode code)
    {
        IsSuccess = isSuccess;
        Error = error;
        Code = code;
    }

    public static Result Success() =>
        new(true, null, ErrorCode.None);

    public static Result Failure(string error, ErrorCode code = ErrorCode.Internal) =>
        new(false, error, code);
}
