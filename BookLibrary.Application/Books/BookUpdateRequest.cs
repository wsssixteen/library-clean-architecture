namespace BookLibrary.Application.Books
{
    public record BookUpdateRequest(int Id, string Title, string Author, decimal Price);
}
