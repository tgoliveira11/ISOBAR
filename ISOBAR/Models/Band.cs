namespace ISOBAR.Models
{
    public class Band
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int NumPlays { get; set; }
        public string Biography { get; set; }
        public string Genre { get; set; }
        public List<string> Albums { get; set; }
        public List<Album> AlbumsList { get; set; }
        
    }
}
