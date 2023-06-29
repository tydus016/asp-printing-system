
using System.ComponentModel.DataAnnotations;

namespace printingsystem.Models.Users
{
    public class Users
    {
        [Key]
        public Guid user_id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string id_number { get; set; }
        public int user_type { get; set; }

    }
}
