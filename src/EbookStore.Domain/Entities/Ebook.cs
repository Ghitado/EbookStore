using EbookStore.Domain.Common;
using EbookStore.Domain.ValueObjects;

namespace EbookStore.Domain.Entities;

public sealed class Ebook
{
	public Guid Id { get; init; }
	public string Title { get; private set; } = string.Empty;
	public string? Description { get; private set; }
	public string? Author { get; private set; }
	public Money Price { get; private set; } = Money.Zero;
	public string FileBlobName { get; private set; } = string.Empty;
	public string? CoverBlobName { get; private set; }
	public DateTime CreatedAt { get; init; }

	private Ebook() { }

	private Ebook(string title, string? description, string? author, Money price, string fileBlobName, string? coverBlobName)
	{
		Id = Guid.NewGuid();
		Title = title;
		Description = description;
		Author = author;
		Price = price;
		FileBlobName = fileBlobName;
		CoverBlobName = coverBlobName;
		CreatedAt = DateTime.UtcNow;
	}

	public static Result<Ebook> Create(
		string title,
		string? description,
		string? author,
		Money price,
		string fileBlobName,
		string? coverBlobName)
	{
		if (string.IsNullOrWhiteSpace(title))
			return Result.Failure<Ebook>(DomainErrors.Ebook.TitleRequired);

		if (price is null or { Value: < 0 })
			return Result.Failure<Ebook>(DomainErrors.Ebook.InvalidPrice);

		if (string.IsNullOrWhiteSpace(fileBlobName))
			return Result.Failure<Ebook>(DomainErrors.Ebook.FileRequired);

		var ebook = new Ebook(title, description, author, price, fileBlobName, coverBlobName);
		return Result.Success(ebook);
	}
}