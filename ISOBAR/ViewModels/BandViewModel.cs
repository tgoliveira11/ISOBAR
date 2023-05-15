using ISOBAR.Models;

namespace ISOBAR.ViewModels
{
    public class BandViewModel
    {
        public List<Band> Bands { get; set; }

        public BandViewModel()
        {
            Bands = new List<Band>();
        }
    }
}
