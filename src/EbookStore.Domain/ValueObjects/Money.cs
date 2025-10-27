using EbookStore.Domain.Common;

namespace EbookStore.Domain.ValueObjects;

public sealed class Money : IEquatable<Money>
{
	public decimal Value { get; }

	private Money(decimal value)
	{
		Value = decimal.Round(value, 2, MidpointRounding.ToEven);
	}

	public static Result<Money> Create(decimal value)
	{
		if (value < 0)
			return Result.Failure<Money>(DomainErrors.Money.InvalidValue);

		return Result.Success(new Money(value));
	}

	public static Money Zero => new(0m);

	public static Money operator +(Money a, Money b) => new(a.Value + b.Value);
	public static Money operator -(Money a, Money b) => new(a.Value - b.Value);
	public static Money operator *(Money a, decimal factor) => new(a.Value * factor);

	public bool Equals(Money? other) => other is not null && Value == other.Value;
	public override bool Equals(object? obj) => obj is Money other && Equals(other);
	public override int GetHashCode() => Value.GetHashCode();

	public override string ToString() => Value.ToString("0.00");
}