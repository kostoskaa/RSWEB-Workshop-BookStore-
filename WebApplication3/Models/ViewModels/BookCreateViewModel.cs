using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication3.Models.ViewModels
{
    public class BookCreateViewModel
    {
        public Book Book { get; set; }
       

        public IFormFile? FrontPageFile { get; set; }
        public IFormFile? EBookFile { get; set; }
    }
}
