namespace printingsystem.Models.Prints
{
    public class Prints
    {
        public Guid id { get; set; }
        public int no_of_pages { get; set; }
        public int paper_count { get; set; }
        public int no_of_copies { get; set; }
        public String type_of_paper { get; set; }
        public String printer_name { get; set; }
        public String files { get; set; }
        public String notes { get; set; }
        public int status { get; set; }

    }
}
