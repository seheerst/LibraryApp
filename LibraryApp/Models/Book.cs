using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LibraryApp.Models
{
    public class Book
    {

        public int BookId { get; set; }

        public string BookName { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public double Price { get; set; }

        [ValidateNever]
        public int TypeId { get; set; }
        [ForeignKey(("TypeId"))]
        
        [ValidateNever]
        public BookType BookType { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }


    }
}
