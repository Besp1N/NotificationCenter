using System.Net;

namespace NotificationCenter.Application.Common;

public readonly struct Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }

    private Result(bool ok, Error error)
    {
        IsSuccess = ok;
        Error = ok ? Error.None : error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static Result Created(string message = "Resource has been created.", object? details = null, string code = "Created")
        => new(true, new Error(code, message, HttpStatusCode.Created, details));
    
    public static Result BadRequest(string message, object? details = null, string code = "BadRequest")
        => Failure(new Error(code, message, HttpStatusCode.BadRequest, details));

    public static Result NotFound(string what, object? key = null, string code = "NotFound")
        => Failure(new Error(code, $"{what} has not been found.", HttpStatusCode.NotFound, new { key }));

    public static Result Conflict(string message, object? details = null, string code = "Conflict")
        => Failure(new Error(code, message, HttpStatusCode.Conflict, details));

    public static Result Unauthorized(string message = "Unauthorized.", string code = "Unauthorized")
        => Failure(new Error(code, message, HttpStatusCode.Unauthorized));

    public static Result Forbidden(string message = "That action is forbidden.", string code = "Forbidden")
        => Failure(new Error(code, message, HttpStatusCode.Forbidden));

    public static Result Unprocessable(string message, object? details = null, string code = "Unprocessable")
        => Failure(new Error(code, message, HttpStatusCode.UnprocessableEntity, details));
}

public readonly struct Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error Error { get; }

    private Result(bool ok, T? value, Error error)
    {
        IsSuccess = ok;
        Value = value;
        Error = ok ? Error.None : error;
    }

    public static Result<T> Success(T value) => new(true, value, Error.None);
    public static Result<T> Failure(Error error) => new(false, default, error);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
    
    public static Result<T> Created(T value, string message = "Resource has been created.", object? details = null, string code = "Created")
        => new(true, value, new Error(code, message, HttpStatusCode.Created, details));

    public static Result<T> BadRequest(string message, object? details = null, string code = "BadRequest")
        => Failure(new Error(code, message, HttpStatusCode.BadRequest, details));

    public static Result<T> NotFound(string what, object? key = null, string code = "NotFound")
        => Failure(new Error(code, $"{what} has not been found.", HttpStatusCode.NotFound, new { key }));

    public static Result<T> Conflict(string message, object? details = null, string code = "Conflict")
        => Failure(new Error(code, message, HttpStatusCode.Conflict, details));

    public static Result<T> Unauthorized(string message = "Unauthorized.", string code = "Unauthorized")
        => Failure(new Error(code, message, HttpStatusCode.Unauthorized));

    public static Result<T> Forbidden(string message = "This action is forbidden.", string code = "Forbidden")
        => Failure(new Error(code, message, HttpStatusCode.Forbidden));

    public static Result<T> Unprocessable(string message, object? details = null, string code = "Unprocessable")
        => Failure(new Error(code, message, HttpStatusCode.UnprocessableEntity, details));
}