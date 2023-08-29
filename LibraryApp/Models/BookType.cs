using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class BookType
    {
        [Key]
        public int TypeId { get; set; }

        [Required]
        public string TypeName { get; set; }

    }
}
