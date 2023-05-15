using ISOBAR.Models;
using ISOBAR.Services;
using ISOBAR.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISOBAR.Pages
{

    public class BandListModel : PageModel
    {
        private readonly IBandService _bandService;
        public BandViewModel BandViewModel { get; set; }

        public int PageIndex { get; set; } = 1;

        public BandListModel(IBandService bandService)
        {
            _bandService = bandService;
        }

        public async Task<IActionResult> OnGetAsync(int pageIndex)
        {
            PageIndex = pageIndex;
            string orderBy = Request.Query["orderBy"];
            var bands = await _bandService.GetBands(PageIndex, orderBy, "");
            return Partial("_BandListPartial", bands);
        }
    }
}
