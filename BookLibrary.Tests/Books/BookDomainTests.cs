using BookLibrary.Domain.Entities;

namespace BookLibrary.Tests.Books
{
    public class BookDomainTests
    {
        [Fact]
        public void Update_Should_Throw_When_Title_Is_Empty()
        {
            // Arrange
            var book = new Book("Old", "Author", 10m);

            // Act + Assert
            Assert.Throws<ArgumentException>(() =>
            {
                book.Update("", "Author", 10m);
            });
        }

        [Fact]
        public void Constructor_Should_Trim_Inputs()
        {
            // Act
            var book = new Book("   Clean Code    ", "   Uncle Bob   ", 50m);

            // Assert
            Assert.Equal("Clean Code", book.Title);
            Assert.Equal("Uncle Bob", book.Author);
        }
    }
}
