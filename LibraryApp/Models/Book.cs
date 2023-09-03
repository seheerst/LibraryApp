using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Book
    {

        public int BookId { get; set; }

        public string BookName { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public Double Price { get; set; }

        public int TypeId { get; set; }
        [ForeignKey(("TypeId"))]
        public BookType BookType { get; set; }

        public string ImageUrl { get; set; }


    }
}
