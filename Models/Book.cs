namespace test_webapi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public bool IsAvailable { get; set; } = true;

        // Navigation properties
        public Author? Author { get; set; }
        public Category? Category { get; set; }
    }
}
