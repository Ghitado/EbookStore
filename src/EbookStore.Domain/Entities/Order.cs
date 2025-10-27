using EbookStore.Domain.Common;
using EbookStore.Domain.ValueObjects;

namespace EbookStore.Domain.Entities;

public sealed class Order
{
	public Guid Id { get; init; }
	public Guid UserId { get; private set; }
	public List<OrderItem> Items { get; private set; } = [];
	public Money TotalAmount { get; private set; } = Money.Zero;
	public DateTime CreatedAt { get; init; }

	private Order() { }

	private Order(Guid userId, List<OrderItem> items)
	{
		Id = Guid.NewGuid();
		UserId = userId;
		Items = items;
		TotalAmount = CalculateTotal(items);
		CreatedAt = DateTime.UtcNow;
	}

	private static Money CalculateTotal(IEnumerable<OrderItem> items)
	{
		var total = Money.Zero;
		foreach (var item in items)
			total += item.Total();

		return total;
	}

	public static Result<Order> Create(Guid userId, List<OrderItem> items)
	{
		if (items is null or { Count: 0 })
			return Result.Failure<Order>(DomainErrors.Order.EmptyOrder);

		var total = CalculateTotal(items);

		if (total.Value < 0)
			return Result.Failure<Order>(DomainErrors.Order.InvalidTotal);

		return Result.Success(new Order(userId, items));
	}

	public void AddItem(OrderItem item)
	{
		if (item is null) return;

		Items.Add(item);
		TotalAmount = CalculateTotal(Items);
	}

	public void RemoveItem(Guid itemId)
	{
		var item = Items.FirstOrDefault(i => i.Id == itemId);
		if (item is null) return;

		Items.Remove(item);
		TotalAmount = CalculateTotal(Items);
	}
}
