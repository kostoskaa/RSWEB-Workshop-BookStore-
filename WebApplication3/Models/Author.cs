using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date of birth")]
        public DateTime BirthDate { get; set; }

        [StringLength(50)]
        public string Nationality { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        public ICollection<Book> Books { get; set; }

        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }
    }
}
