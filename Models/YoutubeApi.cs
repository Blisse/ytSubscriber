using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

namespace ytSubscriber.Models
{
    public class YoutubeApi
    {

        private const string RootYoutubeUrl = "https://www.googleapis.com/youtube/v3";
        private const string PlaylistsUrl = "/playlists";




        private const string OAuthUrl = "https://accounts.google.com/o/oauth2/auth";
        private const string YoutubeOrganizerClientId = "973599681748-130dmm5q9f9fu38r5qr3phsopt1k1c6p.apps.googleusercontent.com";
        

        public void GetAllPlaylists()
        {

        }

        public void OAuthConnect()
        {
            UriBuilder uriBuilder = new UriBuilder(OAuthUrl);
            
            YouTubeService service = new YouTubeService();

            GoogleWebAuthorizationBroker.AuthorizeAsync()
            
        }

        
    }
}
