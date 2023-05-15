using ISOBAR.Models;
using ISOBAR.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISOBAR.Pages
{
    public class BandDetailsModel : PageModel
    {
        private readonly IBandService _bandService;

        public Band Band { get; set; }

        public BandDetailsModel(IBandService bandService)
        {
            _bandService = bandService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string id = Request.Query["id"];
            Band = await _bandService.GetBandDetail(id);

            if (Band == null)
                return NotFound();

            return Page();
        }
    }
}
