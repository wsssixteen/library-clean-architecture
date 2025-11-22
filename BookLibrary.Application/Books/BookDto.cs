namespace BookLibrary.Application.Books
{
    public record BookDto(int Id, string Title, string Author, decimal Price);
}

// This layer coordinates use case and speaks in DTOs (for API needs)