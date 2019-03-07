using Cashback.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyService.Interfaces
{
    public interface ISpotifyService
    {
        string RefreshToken { get; set; }
        string AccesToken { get; set; }
        Task<AuthorizationInfoViewModel> RefreshAccessToken(string refreshToken);
        Task<IEnumerable<GenreViewModel>> ListGenres();
        Task<IEnumerable<AlbumViewModel>> ListAlbuns();
    }
}
