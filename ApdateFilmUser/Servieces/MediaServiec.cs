using ApdateFilmUser.Models;
using ApdateFilmUser.Services.API;
using System;
using System.Collections.Generic;
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
                var mediaResponse = await ApdateFilmUserClient.GetAsync("ApdateFilmUser/media");

                if (String.IsNullOrEmpty(mediaResponse))
                {
                    Console.WriteLine("Ответ от сервера пустой");
                    return null;
                }

                var mediaListResponse = JsonSerializer.Deserialize<List<Media>>(mediaResponse, ApdateFilmUserClient.options);

                return mediaListResponse;
            }
            catch (Exception ex)
            {
                ApdateFilmUserClient.HandleException(ex);
            }

            return null;
        }
    }
}
