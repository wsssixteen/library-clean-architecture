namespace BookLibrary.Domain.Entities
{
    public class Book
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public decimal Price { get; private set; }

        // Domain constructor enfores invariants (Simple Single Responsibility Principle)
        public Book(string title, string author, decimal price) => Update(title, author, price);

        public void Update(string title, string author, decimal price)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author is required.", nameof(author));

            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));

            Title = title.Trim();
            Author = author.Trim();
            Price = price;
        }
    }
}
