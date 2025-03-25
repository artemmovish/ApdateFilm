using ApdateFilmUser.Servieces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApdateFilmUser.ViewModels.AuthViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        private string surname;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private DateTime birthday;

        [ObservableProperty]
        private string avatar;

        public async Task LoadProfile()
        {
            var user = await AuthServiec.GetProfileAsync();

            Surname = user.Surname;
            Name = user.Name;
            Email = user.Email;
            Birthday = user.Birthday;
            Avatar = user.Avatar;
        }
        [RelayCommand]
        async void ToAuth()
        {
            await AuthServiec.LogoutAsync();
            await Shell.Current.GoToAsync("auth");
        }
    }
}
