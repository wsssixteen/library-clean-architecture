namespace BookLibrary.Application.Books
{
    public record BookCreateRequest(int Id, string Title, string Author, decimal Price);
}
