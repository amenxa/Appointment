using System.ComponentModel.DataAnnotations;

namespace Apoint_pro.Data.DTOS
{
    public class UserDTO
    {
       
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
       
        public string? Phone { get; set; }
       
    }
}
