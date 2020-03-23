using System.ComponentModel.DataAnnotations;

namespace njsor.api.Dtos
{
    public class UserForDto
    {
     [Required]
    public string Username { get; set; }
     [Required]
    public string Password { get; set; }
    }
}