using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication3.Models;
using System.Collections.Generic;
namespace WebApplication3.ViewModels
{
    public class BookAuthorGenreViewModel
    {
        public IList<Book> Books { get; set; }
        public SelectList Genres { get; set; }
        public string BookGenres { get; set; }
        public string SearchString { get; set; }
    }
}
