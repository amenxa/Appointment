using System.ComponentModel.DataAnnotations;

namespace Apoint_pro.Data.models
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }
        public string descripton  { get; set; }

        public virtual ICollection<User> users { get; set; }

    }
}
