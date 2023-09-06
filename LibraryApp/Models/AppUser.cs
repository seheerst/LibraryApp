using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class AppUser:IdentityUser
    {
        [Required]
        public int UserNo { get; set; }

        public string? Address { get; set; }

        public string? Fakulte { get; set; }

        public string? Bolum { get; set; }

    }
}
