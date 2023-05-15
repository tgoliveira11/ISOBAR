namespace ISOBAR.Models
{
    public class Track
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public int DurationInSeconds { get; set; }
    }
}
