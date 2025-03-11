using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("UserTypeID")]
        public int UserType { get; set; }
        [JsonIgnore]
        public virtual UserType userType { get; set; }
        [JsonIgnore]
        public virtual Doctor? doctor { get; set; } // if user is a doctor
        [JsonIgnore]
        public virtual ICollection<Apointment> apointments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Cancellation> cancellations { get; set; }
    }
}
