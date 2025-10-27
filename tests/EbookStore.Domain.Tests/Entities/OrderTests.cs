using EbookStore.Domain.Common;
using EbookStore.Domain.Entities;
using EbookStore.Domain.ValueObjects;
using FluentAssertions;

namespace EbookStore.Domain.Tests.Entities;

public class OrderTests
{
	[Fact]
	public void Create_WithValidData_ReturnsSuccess()
	{
		var item = OrderItem.Create(Guid.NewGuid(), Money.Create(10).Value!, 2).Value!;
		var result = Order.Create(Guid.NewGuid(), new List<OrderItem> { item });

		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void Create_WithEmptyItems_ReturnsFailure()
	{
		var result = Order.Create(Guid.NewGuid(), new List<OrderItem>());

		result.IsSuccess.Should().BeFalse();
		result.Error!.Code.Should().Be(DomainErrors.Order.EmptyOrder.Code);
	}

	[Fact]
	public void Create_WithZeroTotal_ReturnsSuccess()
	{
		var item = OrderItem.Create(Guid.NewGuid(), Money.Zero, 1).Value!;
		var result = Order.Create(Guid.NewGuid(), new List<OrderItem> { item });

		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void AddItem_WithValidItem_ReturnsSuccess()
	{
		var item1 = OrderItem.Create(Guid.NewGuid(), Money.Create(10).Value!, 1).Value!;
		var order = Order.Create(Guid.NewGuid(), new List<OrderItem> { item1 }).Value!;

		var item2 = OrderItem.Create(Guid.NewGuid(), Money.Create(5).Value!, 2).Value!;
		order.AddItem(item2);

		order.TotalAmount.Value.Should().Be(10 + 5 * 2);
	}

	[Fact]
	public void RemoveItem_WithExistingItem_ReturnsSuccess()
	{
		var item1 = OrderItem.Create(Guid.NewGuid(), Money.Create(10).Value!, 1).Value!;
		var item2 = OrderItem.Create(Guid.NewGuid(), Money.Create(5).Value!, 2).Value!;
		var order = Order.Create(Guid.NewGuid(), new List<OrderItem> { item1, item2 }).Value!;

		order.RemoveItem(item1.Id);

		order.Items.Should().NotContain(i => i.Id == item1.Id);
		order.TotalAmount.Value.Should().Be(10); // só item2 (5*2)
	}
}

