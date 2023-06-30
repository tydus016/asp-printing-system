using System.ComponentModel.DataAnnotations;

namespace printingsystem.Models.Papers
{
    public class Papers
    {
        [Key]
        public Guid paper_id { get; set; }
        public string paper_name { get; set; }
        public string date { get; set; }
    }
}
