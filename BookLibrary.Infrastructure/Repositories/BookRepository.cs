using BookLibrary.Domain.Entities;
using BookLibrary.Domain.Repositories;
using BookLibrary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _db;

        public BookRepository(LibraryDbContext db) => _db = db;

        public async Task<Book?> GetByIdAsync(int id, CancellationToken ct = default) =>
            await _db.Books.FirstOrDefaultAsync(b => b.Id == id, ct);

        // Use AsNoTracking as a good practice to significantly save resources
        public async Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken ct = default) =>
            await _db.Books.AsNoTracking().ToListAsync(ct);

        public async Task AddAsync(Book book, CancellationToken ct = default) =>
            await _db.Books.AddAsync(book, ct);

        public Task RemoveAsync(Book book, CancellationToken ct = default)
        {
            _db.Books.Remove(book);
            return Task.CompletedTask;
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
            _db.SaveChangesAsync(ct);
    }
}

// Infrastructure depends on EF Core but only exposes IBookRepository to the rest of the system