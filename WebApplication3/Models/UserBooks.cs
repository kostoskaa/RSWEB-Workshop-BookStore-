using WebApplication3.Areas.Identity.Data;

namespace WebApplication3.Models
{
    public class UserBooks
    {
        public int Id { get; set; }
        public string AppUser { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public WebApplication3User BookStoreUser { get; set; }
    }
}