using System.ComponentModel;

namespace WebApplication3.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string GenreName { get; set; }
        public ICollection<BookGenre> BookGenres { get; set; }
    }
}
