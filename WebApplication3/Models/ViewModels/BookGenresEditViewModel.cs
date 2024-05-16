using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication3.Models;
using System.Collections.Generic;
namespace WebApplication3.ViewModels
{
    public class BookGenresEditViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<int>? SelectedGenres { get; set; }
        public IEnumerable<SelectListItem>? GenreList { get; set; }
    }
}