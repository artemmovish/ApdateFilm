using ApdateFilmUser.Models;
using ApdateFilmUser.Services.API;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApdateFilmUser.Servieces
{
    public static class MediaServiec
    {
        public static async Task<List<Media>> GetMediaAsync()
        {
            try
            {
                var mediaResponse = await ApiClient.GetAsync("api/media");

                if (String.IsNullOrEmpty(mediaResponse))
                {
                    Debug.WriteLine("Ответ от сервера пустой");
                    return null;
                }

                var mediaListResponse = JsonSerializer.Deserialize<List<Media>>(mediaResponse, ApiClient.options);

                foreach (var media in mediaListResponse)
                {
                    media.Preview = $"{ApiClient.GetURL()}/storage/{media.Preview}";
                }

                return mediaListResponse;
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }

            return null;
        }
    }
}
