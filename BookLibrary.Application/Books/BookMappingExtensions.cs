using BookLibrary.Domain.Entities;

namespace BookLibrary.Application.Books
{
    internal static class BookMappingExtensions
    {
        public static BookDto ToDto(this Book book) =>
            new(book.Id, book.Title, book.Author, book.Price);
    }
}
