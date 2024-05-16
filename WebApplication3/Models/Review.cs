namespace WebApplication3.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string AppUser { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public Book Book { get; set; }

    }
}
