using System.ComponentModel.DataAnnotations;
namespace SessionWorkshop.Models;

public class Player
{
    [Required(ErrorMessage = "Name is required!")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters long!")]
    public string Name { get; set; }
}