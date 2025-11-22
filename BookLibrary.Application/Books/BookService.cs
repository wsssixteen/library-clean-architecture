using BookLibrary.Domain.Entities;
using BookLibrary.Domain.Repositories;

namespace BookLibrary.Application.Books
{
    //High-level business logic 
    //Single Responsibility: Manage books
    public class BookService
    {
        private readonly IBookRepository _repository;

        // DIP - Depends on abstraction instead of concrete EF class
        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<BookDto>> GetAllAsync(CancellationToken ct = default)
        {
            var books = await _repository.GetAllAsync(ct);
            return books.Select(b => b.ToDto()).ToList();
        }

        public async Task<BookDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var book = await _repository.GetByIdAsync(id, ct);
            return book?.ToDto();
        }

        public async Task<BookDto> CreateAsync(BookCreateRequest request, CancellationToken ct = default)
        {
            var book = new Book(request.Title, request.Author, request.Price);

            await _repository.AddAsync(book, ct);
            await _repository.SaveChangesAsync();

            return book.ToDto();
        }

        public async Task<bool> UpdateAsync(BookUpdateRequest request, CancellationToken ct = default)
        {
            var book = await _repository.GetByIdAsync(request.Id, ct);
            if (book is null) return false;

            book.Update(request.Title, request.Author, request.Price);
            await _repository.SaveChangesAsync(ct);

            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var book = await _repository.GetByIdAsync(id, ct);
            if (book is null) return false;

            await _repository.RemoveAsync(book, ct);
            await _repository.SaveChangesAsync(ct);

            return true;
        }
    }
}

// Follows Single Rsponsibility Principle
// Dependency Inversion Principle (DIP) - Depends on IBookRepository
// Keeps business logic out of Controllers & Entity Framework