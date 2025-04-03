
using ApdateFilmUser.Models;
using ApdateFilmUser.Servieces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApdateFilmUser.ViewModels
{
    public partial class CatalogFiltrViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Media> films;
        [ObservableProperty]
        ObservableCollection<Media> serials;

        [ObservableProperty]
        ObservableCollection<Media> filmsFiltr;
        [ObservableProperty]
        ObservableCollection<Media> serialsFiltr;

        [ObservableProperty]
        ObservableCollection<Genre> genres;

        [ObservableProperty]
        Genre selectedGenre;
        [ObservableProperty]
        DateTime dateStart;
        [ObservableProperty]
        DateTime dateEnd;
        [ObservableProperty]
        string rating;

        public CatalogFiltrViewModel()
        {
            LoadMedia();
        }

        async Task LoadMedia()
        {

            Films = new ObservableCollection<Media>();
            Serials = new ObservableCollection<Media>();

            try
            {
                var list = await MediaServiec.GetMediaAsync();

                var uniqueGenres = new HashSet<Genre>();

                foreach (var media in list)
                {
                    if (media.Type == 1)
                        Serials.Add(media);
                    else
                        Films.Add(media);

                    if (media.Genres != null)
                    {
                        foreach (var genre in media.Genres)
                        {
                            uniqueGenres.Add(genre);
                        }
                    }
                }

                foreach(var g in uniqueGenres)
                {
                    Genres.Add(g);
                }

                FilmsFiltr = new ObservableCollection<Media>(Films);
                SerialsFiltr = new ObservableCollection<Media>(Serials);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Отладка] Ошибка при загрузке медиа: {ex.Message}");
            }
        }

        public void ApplyFilters()
        {
            FilmsFiltr = new ObservableCollection<Media>(FilterMedia(Films));
            SerialsFiltr = new ObservableCollection<Media>(FilterMedia(Serials));
        }

        private IEnumerable<Media> FilterMedia(IEnumerable<Media> source)
        {
            if (source == null) yield break;

            foreach (var media in source)
            {
                bool matchesFilter = true;

                // Фильтрация по жанру (если выбран)
                if (SelectedGenre != null)
                {
                    matchesFilter = media.Genres != null && media.Genres.Contains(SelectedGenre);
                    if (!matchesFilter) continue;
                }

                // Фильтрация по дате начала (если задана)
                if (DateStart != default(DateTime))
                {
                    matchesFilter = media.Release >= DateStart;
                    if (!matchesFilter) continue;
                }

                // Фильтрация по дате окончания (если задана)
                if (DateEnd != default(DateTime))
                {
                    matchesFilter = media.Release <= DateEnd;
                    if (!matchesFilter) continue;
                }

                // Фильтрация по рейтингу (если задан)
                if (!string.IsNullOrEmpty(Rating))
                {
                    if (double.TryParse(Rating, out double ratingValue))
                    {
                        double.TryParse(Rating, out double ratingValueMedia);

                        matchesFilter = ratingValueMedia >= ratingValue;
                        if (!matchesFilter) continue;
                    }
                }

                if (matchesFilter)
                {
                    yield return media;
                }
            }
        }
    }
}
