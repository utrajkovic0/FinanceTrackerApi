using System.ComponentModel.DataAnnotations;

namespace FinanceTrackerApi.DTOs
{
    public class LoginDto
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}
