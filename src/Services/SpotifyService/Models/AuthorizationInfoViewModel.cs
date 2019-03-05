using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyService.Models
{
    public class AuthorizationInfoViewModel
    {
        public string AccessToken { get; internal set; }
        public string RefreshToken { get; internal set; }
        public int ExpiresIn { get; internal set; }
        public string TokenType { get; internal set; }
    }
}
