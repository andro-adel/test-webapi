namespace test_webapi.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Book>? Books { get; set; }
    }
}
