namespace Horizon.Application.Abstractions.Common;
public class BaseResponse<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? Error { get; init; }
    public int Code { get; init; }

    public static BaseResponse<T> FromResult(Result<T> result)
    {
        return new BaseResponse<T>
        {
            Success = result.IsSuccess,
            Data = result.IsSuccess ? result.Value : default,
            Error = result.Error,
            Code = result.IsSuccess ? 200 : MapErrorCodeToStatusCode(result.Code)
        };
    }
    public static BaseResponse<object> FromResult(Result result)
    {
        return new BaseResponse<object>
        {
            Success = result.IsSuccess,
            Data = null,
            Error = result.Error,
            Code = result.IsSuccess ? 200 : MapErrorCodeToStatusCode(result.Code)
        };
    }

    private static int MapErrorCodeToStatusCode(ErrorCode errorCode)
    {
        return errorCode switch
        {
            ErrorCode.NotFound => 404,
            ErrorCode.Validation => 400,
            ErrorCode.Unauthorized => 401,
            ErrorCode.Conflict => 409,
            ErrorCode.Internal => 500,
            _ => 500
        };
    }
}