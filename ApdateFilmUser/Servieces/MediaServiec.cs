using ApdateFilmUser.Models;
using ApdateFilmUser.Models.Response;
using ApdateFilmUser.Services.API;
using System.Diagnostics;
using System.Text.Json;


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

        public static async Task<List<Genre>> GetGenreAsync()
        {
            try
            {
                var genreResponse = await ApiClient.GetAsync("api/genres");

                if (String.IsNullOrEmpty(genreResponse))
                {
                    Debug.WriteLine("Ответ от сервера пустой");
                    return null;
                }

                var mediaListResponse = JsonSerializer.Deserialize<List<Genre>>(genreResponse, ApiClient.options);

                return mediaListResponse;
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }

            return null;
        }

        public static async Task<List<Studio>> GetStudioAsync()
        {
            try
            {
                var genreResponse = await ApiClient.GetAsync("api/studios");

                if (String.IsNullOrEmpty(genreResponse))
                {
                    Debug.WriteLine("Ответ от сервера пустой");
                    return null;
                }

                var mediaListResponse = JsonSerializer.Deserialize<List<Studio>>(genreResponse, ApiClient.options);

                return mediaListResponse;
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }

            return null;
        }

        public static async Task<User> AddToFavoriteAsync(int id)
        {
            try
            {
                var reqwest = new { media_id = id };

                var userResponse = await ApiClient.PostAsync("api/favorites", reqwest);

                if (string.IsNullOrEmpty(userResponse))
                {
                    Debug.WriteLine("Ошибка: Пустой ответ от сервера.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }
            return null;
        }
    }
}
