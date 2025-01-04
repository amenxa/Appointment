using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apoint_pro.Data.models
{
    public class Cancellation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("apointment")]
        public int ApointmentId { get; set; }
        [Required]
        [ForeignKey("user")]  // can be doctor or patient or admin 
        public int canceledBy { get; set; }
        
        public string? Reason { get; set; }

        public virtual Apointment apointment { get; set; }

        public virtual User user { get; set; }

    }
}
