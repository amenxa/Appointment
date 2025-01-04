using System.ComponentModel.DataAnnotations;

namespace Apoint_pro.Data.DTOS
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

}
