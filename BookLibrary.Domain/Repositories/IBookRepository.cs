using BookLibrary.Domain.Entities;

namespace BookLibrary.Domain.Repositories
{
    // Small interface (Interface Segregation)
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Book book, CancellationToken ct = default);
        Task RemoveAsync(Book book, CancellationToken ct = default);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}

// Domain has zero references to external database technologies
// e.g Microsoft.EntityFrameworkCore, System.Data.SqlClient, etc
// Clean Architecture & Dependency Inversion Principle