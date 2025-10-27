using EbookStore.Domain.Common;
using EbookStore.Domain.Entities;
using EbookStore.Domain.Enums;
using FluentAssertions;

namespace EbookStore.Domain.Tests.Entities;

public class UserTests
{
	[Fact]
	public void Create_WithValidEmailAndPassword_ReturnsSuccess()
	{
		// Arrange
		var email = "test@example.com";
		var password = "123456";

		// Act
		var result = User.Create(email, password);

		// Assert
		result.IsSuccess.Should().BeTrue();
		result.Value!.Email.Should().Be(email);
		result.Value.PasswordHash.Should().Be(password);
		result.Value.Role.Should().Be(UserRole.Customer);
	}

	[Fact]
	public void Create_WithEmptyEmail_ReturnsFailure()
	{
		// Arrange
		var email = "";
		var password = "123456";

		// Act
		var result = User.Create(email, password);

		// Assert
		result.IsSuccess.Should().BeFalse();
		result.Error!.Code.Should().Be(DomainErrors.User.EmailRequired.Code);
	}

	[Fact]
	public void Create_WithShortPassword_ReturnsFailure()
	{
		// Arrange
		var email = "test@example.com";
		var password = "123";

		// Act
		var result = User.Create(email, password);

		// Assert
		result.IsSuccess.Should().BeFalse();
		result.Error!.Code.Should().Be(DomainErrors.User.PasswordTooShort.Code);
	}

	[Fact]
	public void UpdateEmail_WithValidEmail_ReturnsSuccess()
	{
		// Arrange
		var user = User.Create("old@example.com", "123456").Value!;
		var newEmail = "new@example.com";

		// Act
		var result = user.UpdateEmail(newEmail);

		// Assert
		result.IsSuccess.Should().BeTrue();
		user.Email.Should().Be(newEmail);
	}

	[Fact]
	public void UpdateEmail_WithEmptyEmail_ReturnsFailure()
	{
		// Arrange
		var user = User.Create("old@example.com", "123456").Value!;
		var newEmail = "";

		// Act
		var result = user.UpdateEmail(newEmail);

		// Assert
		result.IsSuccess.Should().BeFalse();
		result.Error!.Code.Should().Be(DomainErrors.User.EmailRequired.Code);
	}

	[Fact]
	public void UpdatePassword_WithValidPassword_ReturnsSuccess()
	{
		// Arrange
		var user = User.Create("test@example.com", "123456").Value!;
		var newPassword = "654321";

		// Act
		var result = user.UpdatePassword(newPassword);

		// Assert
		result.IsSuccess.Should().BeTrue();
		user.PasswordHash.Should().Be(newPassword);
	}

	[Fact]
	public void UpdatePassword_WithShortPassword_ReturnsFailure()
	{
		// Arrange
		var user = User.Create("test@example.com", "123456").Value!;
		var newPassword = "123";

		// Act
		var result = user.UpdatePassword(newPassword);

		// Assert
		result.IsSuccess.Should().BeFalse();
		result.Error!.Code.Should().Be("USER_PASSWORD_TOO_SHORT");
	}

	[Fact]
	public void IsAdmin_WhenRoleIsCustomer_ReturnsFalse()
	{
		// Arrange
		var user = User.Create("test@example.com", "123456").Value!;

		// Act
		var isAdmin = user.IsAdmin();

		// Assert
		isAdmin.Should().BeFalse();
	}
}

