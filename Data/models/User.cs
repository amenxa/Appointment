using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apoint_pro.Data.models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }
        public string ? phone { get; set; }

        [ForeignKey("userType")]
        public int UserType { get; set; }

        public virtual UserType userType { get; set; }

        public virtual Doctor? doctor { get; set; } // if user is a doctor

        public virtual ICollection<Apointment> apointments { get; set; }

        public virtual ICollection<Cancellation> cancellations { get; set; }
    }
}
