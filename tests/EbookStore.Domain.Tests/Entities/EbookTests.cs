using EbookStore.Domain.Common;
using EbookStore.Domain.Entities;
using EbookStore.Domain.ValueObjects;
using FluentAssertions;

namespace EbookStore.Domain.Tests.Entities;

public class EbookTests
{
	[Fact]
	public void Create_WithValidData_ReturnsSuccess()
	{
		var price = Money.Create(10).Value!;
		var result = Ebook.Create("Title", "Desc", "Author", price, "file.pdf", null);

		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void Create_WithEmptyTitle_ReturnsFailure()
	{
		var price = Money.Create(10).Value!;
		var result = Ebook.Create("", "Desc", "Author", price, "file.pdf", null);

		result.IsSuccess.Should().BeFalse();
		result.Error!.Code.Should().Be(DomainErrors.Ebook.TitleRequired.Code);
	}

	[Fact]
	public void Create_WithNegativePrice_ReturnsFailure()
	{
		var price = Money.Create(-1).Value!;
		var result = Ebook.Create("Title", "Desc", "Author", price, "file.pdf", null);

		result.IsSuccess.Should().BeFalse();
		result.Error!.Code.Should().Be(DomainErrors.Ebook.InvalidPrice.Code);
	}

	[Fact]
	public void Create_WithEmptyFile_ReturnsFailure()
	{
		var price = Money.Create(10).Value!;
		var result = Ebook.Create("Title", "Desc", "Author", price, "", null);

		result.IsSuccess.Should().BeFalse();
		result.Error!.Code.Should().Be(DomainErrors.Ebook.FileRequired.Code);
	}
}

