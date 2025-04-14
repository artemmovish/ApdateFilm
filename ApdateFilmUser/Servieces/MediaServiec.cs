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
        public static async Task<Media> GetMediaAsync(int id)
        {
            try
            {
                var mediaReqwest = await ApiClient.GetAsync($"api/media/{id}");

                if (String.IsNullOrEmpty(mediaReqwest))
                {
                    Debug.WriteLine("Ответ от сервера пустой");
                    return null;
                }

                var mediaResponse = JsonSerializer.Deserialize<MediaResponse>(mediaReqwest, ApiClient.options);

                return mediaResponse.Media;
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

        //public static async Task<User> DeleteToFavoriteAsync(int id)
        //{
        //    try
        //    {
        //        var reqwest = new { media_id = id };

        //        var userResponse = await ApiClient.DeleteAsync($"api/favorites/{id}");

        //        if (userResponse)
        //        {
        //            Debug.WriteLine("[Отладка] Ошибка: Пустой ответ от сервера.");
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ApiClient.HandleException(ex);
        //    }
        //    return null;
        //}
        public static async Task<bool> CheckFavoriteAsync(int id)
        {
            try
            {
                var checkResponse = await ApiClient.GetAsync($"api/favorites/{id}/exist");

                if (string.IsNullOrEmpty(checkResponse))
                {
                    Debug.WriteLine("[Отладка] Ошибка: Пустой ответ от сервера.");
                    return false;
                }

                return checkResponse == "1" ? true : false;
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }
            return false;
        }

        public static async Task DeleteFromFavoriteAsync(int id)
        {
            try
            {
                var checkResponse = await ApiClient.GetAsync($"api/favorites/{id}");

                if (string.IsNullOrEmpty(checkResponse))
                {
                    Debug.WriteLine("[Отладка] Ошибка: Пустой ответ от сервера.");
                    return;
                }
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }
        }
        public static async Task<bool> AddReviewAsync(int id, string text, int rating)
        {
            try
            {
                var checkResponse = await ApiClient.PostAsync($"api/reviews?media_id={id}&text={text}&rating={rating}", "");

                if (string.IsNullOrEmpty(checkResponse))
                {
                    Debug.WriteLine("[Отладка] Ошибка: Пустой ответ от сервера.");

                    await Shell.Current.DisplayAlert("Ошибка", "Отзыв не добавлен", "ОК");
                }
                await Shell.Current.DisplayAlert("Успех", "Отзыв добавлен", "ОК");
                return true;
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
                await Shell.Current.DisplayAlert("Ошибка", "Отзыв не добавлен", "ОК");
            }
            return false;
        }
        public static async Task<List<FavoriteMedia>> GetMediaFavoriteAsync()
        {
            try
            {
                var mediaResponse = await ApiClient.GetAsync("api/favorites");

                if (String.IsNullOrEmpty(mediaResponse))
                {
                    Debug.WriteLine("Ответ от сервера пустой");
                    return null;
                }

                var mediaListResponse = JsonSerializer.Deserialize<List<FavoriteMedia>>(mediaResponse, ApiClient.options);


                foreach (var media in mediaListResponse)
                {
                    media.Media.Preview = $"{ApiClient.GetURL()}/storage/{media.Media.Preview}";
                }

                return mediaListResponse;
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }

            return null;
        }

        public static async Task<Actor> GetActorAsync(int id)
        {
            try
            {
                var mediaReqwest = await ApiClient.GetAsync($"api/actors/{id}");

                if (String.IsNullOrEmpty(mediaReqwest))
                {
                    Debug.WriteLine("Ответ от сервера пустой");
                    return null;
                }

                var mediaResponse = JsonSerializer.Deserialize<ActorResponse>(mediaReqwest, ApiClient.options);

                return mediaResponse.Actor;
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }

            return null;
        }
    }
}
