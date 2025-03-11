using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apoint_pro.Data.models
{
    public class Apointment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("user")]
        [Required]
        public int UserId { get; set; }
        [Required]
        [ForeignKey("doctor")]
        public int DoctorId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string? Notes { get; set; }

        public string status { get; set; } = "pending";

        public virtual User  user { get; set; }
        public virtual Doctor doctor { get; set; }

    }
}
