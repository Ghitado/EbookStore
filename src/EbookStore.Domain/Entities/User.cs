using EbookStore.Domain.Common;
using EbookStore.Domain.Enums;

namespace EbookStore.Domain.Entities;

public sealed class User
{
	public Guid Id { get; init; }
	public string Email { get; private set; } = string.Empty;
	public string PasswordHash { get; private set; } = string.Empty;
	public UserRole Role { get; private set; }
	public DateTime CreatedAt { get; init; }

	private User() { }

	private User(string email, string passwordHash)
	{
		Id = Guid.NewGuid();
		Email = email;
		PasswordHash = passwordHash;
		Role = UserRole.Customer;
		CreatedAt = DateTime.UtcNow;
	}

	public static Result<User> Create(string email, string passwordHash)
	{
		if (string.IsNullOrWhiteSpace(email))
			return Result.Failure<User>(DomainErrors.User.EmailRequired);

		if (string.IsNullOrWhiteSpace(passwordHash) || passwordHash.Length is < 6)
			return Result.Failure<User>(DomainErrors.User.PasswordTooShort);

		var user = new User(email, passwordHash);
		return Result.Success(user);
	}

	public Result UpdateEmail(string newEmail)
	{
		if (string.IsNullOrWhiteSpace(newEmail))
			return Result.Failure(DomainErrors.User.EmailRequired);

		Email = newEmail;
		return Result.Success();
	}

	public Result UpdatePassword(string newPasswordHash)
	{
		if (string.IsNullOrWhiteSpace(newPasswordHash) || newPasswordHash.Length is < 6)
			return Result.Failure(DomainErrors.User.PasswordTooShort);

		PasswordHash = newPasswordHash;
		return Result.Success();
	}

	public bool IsAdmin() => Role == UserRole.Admin;
}

