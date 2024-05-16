using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [DisplayName("Year Published")]
        public int? YearPublished { get; set; }

        [DisplayName("Number of Pages")]
        public int? NumPages { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Publisher { get; set; }

        [Url]
        [DisplayName("Front Page")]
        public string? FrontPage { get; set; }

        [Url]
        [DisplayName("Download")]
        public string? DownloadUrl { get; set; }

        [DisplayName("Average Rating")]
        public double AverageRating()
        {
            if (Reviews == null || Reviews.Count == 0)
            {
                return 0;
            }
            int sumOfRatings = 0;
            foreach (var item in Reviews)
            {
                if (item.Rating != null)
                {
                    sumOfRatings += (int)item.Rating;

                }
            }
            double averageRating = (double)sumOfRatings / Reviews.Count;
            return averageRating;
        }

        [DisplayName("Author")]
        public int AuthorId { get; set; }

        public Author? Author { get; set; }



        public ICollection<BookGenre> Genres { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserBooks> UserBooks { get; set; }
    }
}
