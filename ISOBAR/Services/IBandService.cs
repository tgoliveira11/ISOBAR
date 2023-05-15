using ISOBAR.Models;

namespace ISOBAR.Services
{
    public interface IBandService
    {
        Task<List<Band>> GetBands(int? pageIndex, string? orderBy, string? searchQuery);
        Task<Band> GetBandDetail(string bandId);
        Task<Album> GetAlbumDetail(string albumId);
        Task<List<Album>> GetAlbumsForBand(Band band);
        Task<Album> GetAlbumById(string albumId);
    }

}
