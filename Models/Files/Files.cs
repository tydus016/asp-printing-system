using System.ComponentModel.DataAnnotations;

namespace printingsystem.Models.Files
{
    public class Files
    {
        [Key]
        public Guid file_id { get; set; }
        public int fk_user_id { get; set; }
        public string filename { get; set; }
        public int copies { get; set; }
        public string paper_type { get; set; }
        public string printer { get; set; }
        public int status { get; set; }
    }
}
