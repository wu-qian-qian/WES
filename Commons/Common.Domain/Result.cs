namespace Common.Domain;

public class Result
{
    public Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }

    public string Message { get; }

    public static Result Success()
    {
        return new Result(true, string.Empty);
    }

    public static Result Error(string messsage)
    {
        return new Result(false, messsage);
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, string.Empty);
    }

    public static Result<TValue> Error<TValue>(string message)
    {
        return new Result<TValue>(default, false, message);
    }
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? value, bool isSuccess, string error)
        : base(isSuccess, error)
    {
        _value = value;
    }
}