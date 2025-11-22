using BookLibrary.Application.Books;
using BookLibrary.Domain.Entities;
using BookLibrary.Domain.Repositories;
using NSubstitute;

namespace BookLibrary.Tests.Books
{
    public class BookServiceTests
    {
        private readonly IBookRepository _repo;
        private readonly BookService _service;

        public BookServiceTests()
        {
            _repo = Substitute.For<IBookRepository>();
            _service = new BookService(_repo);
        }

        // ---------------------------
        // GetAllAsync
        // ---------------------------
        [Fact]
        public async Task GetAllAsync_Should_Return_Mapped_Dtos()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book("A", "Author1", 10),
                new Book("B", "Author2", 20)
            };

            _repo.GetAllAsync(Arg.Any<CancellationToken>()).Returns(books);

            // Act 
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("A", result[0].Title);
            Assert.Equal("B", result[1].Title);
        }

        // ---------------------------
        // GetByIdAsync
        // ---------------------------
        [Fact]
        public async Task GetByIdAsycn_Should_Return_Dto_When_Found()
        {
            // Arrange
            var book = new Book("Clean Code", "Bob", 50);
            _repo.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(book);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Clean Code", result!.Title);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Null_When_Not_Found()
        {
            // Arrange
            _repo.GetByIdAsync(1, Arg.Any<CancellationToken>())
                 .Returns((Book?)null);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        // ---------------------------
        // CreateAsync
        // ---------------------------
        [Fact]
        public async Task CreateAsync_Should_Add_Book_And_Return_Dto()
        {
            // Arrange
            var request = new BookCreateRequest(1, "A", "Author", 10);

            // Act
            var result = await _service.CreateAsync(request);

            // Assert: DTO correctness
            Assert.Equal("A", result.Title);
            Assert.Equal("Author", result.Author);
            Assert.Equal(10, result.Price);

            // Assert: repository interactions
            await _repo.Received(1).AddAsync(
                Arg.Is<Book>(b =>
                    b.Title == "A" &&
                    b.Author == "Author" &&
                    b.Price == 10),
                Arg.Any<CancellationToken>());

            await _repo.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        // ---------------------------
        // UpdateAsync
        // ---------------------------
        [Fact]
        public async Task UpdateAsync_Should_Return_True_When_Update_Succeeds()
        {
            // Arrange
            var book = new Book("Old", "Bob", 10);
            _repo.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(book);

            var request = new BookUpdateRequest(1, "New", "Bob", 20);

            // Act
            var result = await _service.UpdateAsync(request);

            // Assert
            Assert.True(result);
            Assert.Equal("New", book.Title);
            Assert.Equal(20, book.Price);

            await _repo.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_False_When_Book_Not_Found()
        {
            // Arrange
            _repo.GetByIdAsync(1, Arg.Any<CancellationToken>())
                 .Returns((Book?)null);

            var request = new BookUpdateRequest(1, "New", "Bob", 20);

            // Act
            var result = await _service.UpdateAsync(request);

            // Assert
            Assert.False(result);

            await _repo.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        }


        // ---------------------------
        // DeleteAsync
        // ---------------------------
        [Fact]
        public async Task DeleteAsync_Should_Return_True_When_Deleted()
        {
            // Arrange
            var book = new Book("X", "Y", 10);
            _repo.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(book);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);

            await _repo.Received(1).RemoveAsync(book, Arg.Any<CancellationToken>());
            await _repo.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_False_When_Not_Found()
        {
            // Arrange
            _repo.GetByIdAsync(1, Arg.Any<CancellationToken>())
                 .Returns((Book?)null);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.False(result);

            await _repo.DidNotReceive().RemoveAsync(Arg.Any<Book>(), Arg.Any<CancellationToken>());
        }
    }
}
