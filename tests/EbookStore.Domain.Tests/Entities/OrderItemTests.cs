using EbookStore.Domain.Common;
using EbookStore.Domain.Entities;
using EbookStore.Domain.ValueObjects;
using FluentAssertions;

namespace EbookStore.Domain.Tests.Entities;

public class OrderItemTests
{
	[Fact]
	public void Create_WithValidData_ReturnsSuccess()
	{
		var price = Money.Create(10).Value!;
		var result = OrderItem.Create(Guid.NewGuid(), price, 1);

		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void Create_WithZeroQuantity_ReturnsFailure()
	{
		var price = Money.Create(10).Value!;
		var result = OrderItem.Create(Guid.NewGuid(), price, 0);

		result.IsSuccess.Should().BeFalse();
		result.Error!.Code.Should().Be(DomainErrors.OrderItem.InvalidQuantity.Code);
	}

	[Fact]
	public void Create_WithZeroPrice_ReturnsFailure()
	{
		var price = Money.Zero;
		var result = OrderItem.Create(Guid.NewGuid(), price, 1);

		result.IsSuccess.Should().BeFalse();
		result.Error!.Code.Should().Be(DomainErrors.OrderItem.InvalidUnitPrice.Code);
	}

	[Fact]
	public void Total_WithValidData_ReturnsCorrectAmount()
	{
		var price = Money.Create(10).Value!;
		var item = OrderItem.Create(Guid.NewGuid(), price, 2).Value!;

		item.Total().Value.Should().BeApproximately(20m, 0.01m);
	}
}