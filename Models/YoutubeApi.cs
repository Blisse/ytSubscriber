using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

namespace ytSubscriber.Models
{
    public class YoutubeApi
    {
        public void GetAllPlaylists()
        {

        }

        public async void OAuthConnect()
        {
            UserCredential credientials;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credientials = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.Youtube },
                    "Blissea",
                    CancellationToken.None);
            }

            YouTubeService service = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credientials,
                ApplicationName = "YoutubeOrganizer"
            });

            PlaylistsResource.ListRequest listRequest = service.Playlists.List("id");
            listRequest.MaxResults = 50;
            listRequest.Mine = true;

            PlaylistListResponse listResponse = await listRequest.ExecuteAsync();

            var count = listResponse.Items.Count;

            Debug.WriteLine(count);
        }
    }
}
