using EbookStore.Domain.Common;
using EbookStore.Domain.ValueObjects;

namespace EbookStore.Domain.Entities;

public sealed class OrderItem
{
	public Guid Id { get; init; }
	public Guid EbookId { get; private set; }
	public Money UnitPrice { get; private set; } = Money.Zero;
	public int Quantity { get; private set; }

	private OrderItem() { }

	private OrderItem(Guid ebookId, Money unitPrice, int quantity)
	{
		Id = Guid.NewGuid();
		EbookId = ebookId;
		UnitPrice = unitPrice;
		Quantity = quantity;
	}

	public static Result<OrderItem> Create(Guid ebookId, Money unitPrice, int quantity)
	{
		if (quantity is <= 0)
			return Result.Failure<OrderItem>(DomainErrors.OrderItem.InvalidQuantity);

		if (unitPrice.Value is < 0)
			return Result.Failure<OrderItem>(DomainErrors.OrderItem.InvalidUnitPrice);

		var item = new OrderItem(ebookId, unitPrice, quantity);
		return Result.Success(item);
	}

	public Money Total() => UnitPrice * Quantity;
}

