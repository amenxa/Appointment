using Org.BouncyCastle.Asn1.Cms;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apoint_pro.Data.models
{
    public class Doctor
    {
        [Key]
        [ForeignKey("user")]
        public int Id { get; set; }
        [Required]
        public string speciality { get; set; }
        [Required]
        public TimeSpan timeFrom { get; set; }
        [Required]
        public TimeSpan timeTo { get; set; }

        public virtual User user { get; set; }

        public virtual ICollection<Apointment> apointments { get; set; }



    }
}
