namespace EbookStore.Domain.Common;

public sealed class Error
{
	public string Code { get; }
	public string Message { get; }

	public Error(string code, string message)
	{
		Code = code;
		Message = message;
	}

	public override string ToString() => $"{Code}: {Message}";
}

