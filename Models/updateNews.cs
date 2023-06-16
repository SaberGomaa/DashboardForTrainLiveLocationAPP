namespace Dashboard.Models
{
    public class updateNews
    {
        public int Id { get; set; }
        public string image { get; set; }
        public DateTime? date { get; set; } = DateTime.Now;
        public string ContentOfPost { get; set; }
        public string? Title { get; set; }

    }
}