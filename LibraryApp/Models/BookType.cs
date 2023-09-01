using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class BookType
    {
        [Key]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Type Name is required")]
        [MaxLength(20)]
        public string? TypeName { get; set; }

    }
}
