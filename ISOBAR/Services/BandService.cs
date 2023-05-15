using ISOBAR.Models;
using ISOBAR.Pages;
using Newtonsoft.Json;

namespace ISOBAR.Services
{

    public class BandService : IBandService
    {
        private readonly HttpClient _httpClient;
        private const int PageSize = 20;

        public BandService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Band>> GetBands(int? pageIndex, string? orderBy, string? searchQuery)
        {
            pageIndex ??= 0;
            var response = await _httpClient.GetAsync("/api/bands");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var allBands = JsonConvert.DeserializeObject<List<Band>>(jsonString);

                if(!string.IsNullOrEmpty(searchQuery))
                {
                    allBands = allBands?.Where(band => band.Name.ToLower().Contains(searchQuery.ToLower())).ToList();
                }

                // Ajust order
                if (string.Equals(orderBy, "alphabetical", StringComparison.OrdinalIgnoreCase))
                {
                    allBands =  GetBandsOrderedAlphabeticallyAsync(allBands);
                }
                else if (string.Equals(orderBy, "popularity", StringComparison.OrdinalIgnoreCase))
                {
                    allBands = GetBandsOrderedByPlaysAsync(allBands);
                }

                // Apply client-side pagination
                var startIndex = ((int)pageIndex - 1) * PageSize;
                var bands = allBands?.Skip(startIndex).Take(PageSize).ToList();

                return bands;
            }
            else
            {
                throw new Exception("Error fetching data from API");
            }
        }

        public async Task<Band> GetBandDetail(string bandId)
        {
            var response = await _httpClient.GetAsync($"/api/bands/{bandId}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var band = JsonConvert.DeserializeObject<Band>(stringResponse);
            var albums = await GetAlbumsForBand(band);
            band.AlbumsList = albums;
            return band;
        }

        public async Task<Album> GetAlbumDetail(string albumId)
        {
            var response = await _httpClient.GetAsync($"/api/albums/{albumId}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Album>(stringResponse);
        }

        public async Task<List<Album>> GetAlbumsForBand(Band band)
        {
            List<Album> albums = new List<Album>();

            foreach (string albumId in band.Albums)
            {
                Album album = await GetAlbumById(albumId);
                albums.Add(album);
            }

            return albums;
        }

        public async Task<Album> GetAlbumById(string albumId)
        {
            string url = $"/api/albums/{albumId}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Album album = JsonConvert.DeserializeObject<Album>(content);
                return album;
            }

            // Handle error cases here...
            // For example, log the error or throw an exception.
            // You can also return null or a default Album object based on your requirement.
            return null;
        }

        private List<Band> GetBandsOrderedAlphabeticallyAsync(List<Band>? bands)
        {
            return bands?.OrderBy(band => band.Name).ToList();
        }

        private List<Band> GetBandsOrderedByPlaysAsync(List<Band>? bands)
        {
            return bands?.OrderByDescending(band => band.NumPlays).ToList();
        }

        private List<Band> GetBandsOrderedAlphabeticallyReverseAsync(List<Band>? bands)
        {
            return bands?.OrderByDescending(band => band.Name).ToList();
        }

        private List<Band> GetBandsOrderedByPlaysReverseAsync(List<Band>? bands)
        {
            return bands?.OrderBy(band => band.NumPlays).ToList();
        }
    }

}
