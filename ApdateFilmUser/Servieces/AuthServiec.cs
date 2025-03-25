using ApdateFilmUser.Models;
using ApdateFilmUser.Models.Response;
using ApdateFilmUser.Services.API;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApdateFilmUser.Servieces
{
    public static class AuthServiec
    {
        public static async Task<User> RegisterAsync(User userRequest)
        {
            try
            {
                var us = new
                {
                    surname = userRequest.Surname,
                    name = userRequest.Name,
                    email = userRequest.Email,
                    password = userRequest.Password,
                    birthday = userRequest.Birthday
                };

                var userResponse = await ApiClient.PostAsync("api/register", us);

                if (string.IsNullOrEmpty(userResponse))
                {
                    Debug.WriteLine("Ошибка: Пустой ответ от сервера.");
                    return null;
                }

                var authResponse = JsonSerializer.Deserialize<AuthResponse>(userResponse, ApiClient.options);

                if (!string.IsNullOrEmpty(authResponse.Token))
                {
                    // Устанавливаем токен в ApdateFilmUserClient
                    ApiClient.SetToken(authResponse.Token);
                    Debug.WriteLine("Токен успешно установлен.");
                    return authResponse.User;
                }
                else
                {
                    Debug.WriteLine("Токен не обнаружен в ответе.");
                }
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }
            return null;
        }

        public static async Task<User> UpdateAsync(User userRequest)
        {
            try
            {
                var userResponse = await ApiClient.PostAsync("api/profile", userRequest);

                if (string.IsNullOrEmpty(userResponse))
                {
                    Debug.WriteLine("Ошибка: Пустой ответ от сервера.");
                    return null;
                }

                var authResponse = JsonSerializer.Deserialize<AuthResponse>(userResponse, ApiClient.options);

                if (!string.IsNullOrEmpty(authResponse.Token))
                {
                    // Устанавливаем токен в ApdateFilmUserClient
                    ApiClient.SetToken(authResponse.Token);
                    Debug.WriteLine("Токен успешно установлен.");
                    return authResponse.User;
                }
                else
                {
                    Debug.WriteLine("Токен не обнаружен в ответе.");
                }
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }
            return null;
        }

        public static async Task<User> LoginAsync(User userRequest)
        {
            try
            {
                var userResponse = await ApiClient.PostAsync("api/login", userRequest);

                if (string.IsNullOrEmpty(userResponse))
                {
                    Debug.WriteLine("Ошибка: Пустой ответ от сервера.");
                    return null;
                }

                var authResponse = JsonSerializer.Deserialize<AuthResponse>(userResponse, ApiClient.options);

                if (!string.IsNullOrEmpty(authResponse.Token))
                {
                    // Устанавливаем токен в ApdateFilmUserClient
                    ApiClient.SetToken(authResponse.Token);
                    Debug.WriteLine("Токен успешно установлен.");
                    return authResponse.User;
                }
                else
                {
                    Debug.WriteLine("Токен не обнаружен в ответе.");
                }
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }
            return null;
        }
        
        public static async Task<User> GetProfileAsync()
        {
            try
            {
                var userResponse = await ApiClient.GetAsync("api/profile");

                if (string.IsNullOrEmpty(userResponse))
                {
                    Debug.WriteLine("Ошибка: Пустой ответ от сервера.");
                    return null;
                }

                var authResponse = JsonSerializer.Deserialize<UserResponse>(userResponse, ApiClient.options);

                return authResponse.User;
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }
            return null;
        }

        public static async Task LogoutAsync()
        {
            try
            {
                var userResponse = await ApiClient.GetAsync("api/logout");

                if (string.IsNullOrEmpty(userResponse))
                {
                    Debug.WriteLine("Ошибка: Пустой ответ от сервера.");
                }
            }
            catch (Exception ex)
            {
                ApiClient.HandleException(ex);
            }
        }
    }
}
