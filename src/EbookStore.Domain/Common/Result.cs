using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EbookStore.Domain.Common;

public sealed class Result
{
	public bool IsSuccess { get; }
	public Error? Error { get; }

	private Result(bool isSuccess, Error? error)
	{
		IsSuccess = isSuccess;
		Error = error;
	}

	public static Result Success() => new(true, null);
	public static Result Failure(Error error) => new(false, error);

	public static Result<T> Success<T>(T value) => new(value, true, null);
	public static Result<T> Failure<T>(Error error) => new(default!, false, error);
}

public sealed class Result<T>
{
	public T? Value { get; }
	public bool IsSuccess { get; }
	public Error? Error { get; }

	internal Result(T? value, bool isSuccess, Error? error)
	{
		Value = value;
		IsSuccess = isSuccess;
		Error = error;
	}
}

