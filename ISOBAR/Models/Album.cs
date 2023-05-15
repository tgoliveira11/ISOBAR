using Newtonsoft.Json;

namespace ISOBAR.Models
{
    public class Album
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime ReleasedDate { get; set; }
    }
}
