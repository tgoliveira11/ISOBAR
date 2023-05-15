using ISOBAR.Models;
using ISOBAR.Services;
using ISOBAR.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISOBAR.Pages
{

    public class IndexModel : PageModel
    {
        private readonly IBandService _bandService;

        public BandViewModel BandViewModel { get; set; }

        public int PageIndex { get; set; } = 1;

        public List<Band> Bands { get; set; }

        public IndexModel(IBandService bandService)
        {
            _bandService = bandService;
        }

        public async Task<IActionResult> OnGetAsync(string orderBy, string searchQuery)
        {
            if (string.Equals(orderBy, "alphabetical", StringComparison.OrdinalIgnoreCase))
            {
                PageIndex = 1;
            }
            else if (string.Equals(orderBy, "popularity", StringComparison.OrdinalIgnoreCase))
            {
                PageIndex = 1;
            }
            Bands = await _bandService.GetBands(PageIndex, orderBy, searchQuery);
            return Page();
        }
    }
}
