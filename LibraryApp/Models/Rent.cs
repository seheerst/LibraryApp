using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LibraryApp.Models;

public class Rent
{
    public int RentId { get; set; }
    
    public int UserId { get; set; }

    [ValidateNever]
    [ForeignKey("BookId")]
    public int BookId { get; set; }
    
    [ValidateNever]
    public Book Book { get; set; }
}