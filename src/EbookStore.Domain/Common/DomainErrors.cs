namespace EbookStore.Domain.Common;

public static class DomainErrors
{
	public static class Ebook
	{
		public static readonly Error TitleRequired =
			new("EBOOK_TITLE_REQUIRED", "Title is required.");

		public static readonly Error InvalidPrice =
			new("EBOOK_INVALID_PRICE", "Price cannot be negative.");

		public static readonly Error FileRequired =
			new("EBOOK_FILE_REQUIRED", "File must be provided.");
	}

	public static class User
	{
		public static readonly Error EmailRequired =
			new("USER_EMAIL_REQUIRED", "Email is required.");

		public static readonly Error PasswordTooShort =
			new("USER_PASSWORD_TOO_SHORT", "Password must be at least 6 characters.");

		public static readonly Error InvalidRole =
			new("USER_INVALID_ROLE", "Invalid user role.");
	}

	public static class Order
	{
		public static readonly Error EmptyOrder =
			new("ORDER_EMPTY", "Order must contain at least one item.");

		public static readonly Error InvalidTotal =
			new("ORDER_INVALID_TOTAL", "Total must be cannot be negative.");
	}

	public static class OrderItem
	{
		public static readonly Error InvalidQuantity =
			new("ORDER_ITEM_INVALID_QTY", "Quantity must be at least one.");

		public static readonly Error InvalidUnitPrice =
			new("ORDER_ITEM_INVALID_PRICE", "Unit price cannot be negative.");
	}

	public static class Money
	{
		public static readonly Error InvalidValue =
			new("MONEY_INVALID_VALUE", "Value cannot be negative");
	}
}

