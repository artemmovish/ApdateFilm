using ApdateFilmUser.Models;
using ApdateFilmUser.Models.Response;
using ApdateFilmUser.Services.API;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApdateFilmUser.Servieces
{
    public static class AuthServiec
    {
        public static async Task<User> AuthAsync(User userRequest)
        {
            try
            {
                var userResponse = await ApdateFilmUserClient.PostAsync("ApdateFilmUser/register", userRequest);

                if (string.IsNullOrEmpty(userResponse))
                {
                    Console.WriteLine("Ошибка: Пустой ответ от сервера.");
                    return null;
                }

                var authResponse = JsonSerializer.Deserialize<AuthResponse>(userResponse, ApdateFilmUserClient.options);

                if (!string.IsNullOrEmpty(authResponse.Token))
                {
                    // Устанавливаем токен в ApdateFilmUserClient
                    ApdateFilmUserClient.SetToken(authResponse.Token);
                    Console.WriteLine("Токен успешно установлен.");
                    return authResponse.User;
                }
                else
                {
                    Console.WriteLine("Токен не обнаружен в ответе.");
                }
            }
            catch (Exception ex)
            {
                ApdateFilmUserClient.HandleException(ex);
            }
            return null;
        }

        public static async Task<User> LoginAsync(User userRequest)
        {
            try
            {
                var userResponse = await ApdateFilmUserClient.PostAsync("ApdateFilmUser/login", userRequest);

                if (string.IsNullOrEmpty(userResponse))
                {
                    Console.WriteLine("Ошибка: Пустой ответ от сервера.");
                    return null;
                }

                var authResponse = JsonSerializer.Deserialize<AuthResponse>(userResponse, ApdateFilmUserClient.options);

                if (!string.IsNullOrEmpty(authResponse.Token))
                {
                    // Устанавливаем токен в ApdateFilmUserClient
                    ApdateFilmUserClient.SetToken(authResponse.Token);
                    Console.WriteLine("Токен успешно установлен.");
                    return authResponse.User;
                }
                else
                {
                    Console.WriteLine("Токен не обнаружен в ответе.");
                }
            }
            catch (Exception ex)
            {
                ApdateFilmUserClient.HandleException(ex);
            }
            return null;
        }
        

    }
}
