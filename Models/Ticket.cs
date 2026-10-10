namespace ServiceDeskPro.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Priority { get; set; } = "Low";
        public string Status { get; set; } = "Open";
    }
}