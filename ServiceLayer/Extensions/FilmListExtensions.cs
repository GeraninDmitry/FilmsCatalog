using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModels;

namespace ServiceLayer.Extensions
{
    public static class FilmListExtensions
    {
        public static IQueryable<Film> Page(this IQueryable<Film> list, int page, int pageSize)
        {
            list = list.Skip((page - 1) * pageSize);

            var items = list.Take(pageSize);

            return items;
        }

        public static IEnumerable<FilmViewModel> ToViewModel(this IEnumerable<Film> list)
        {
            return list.Select(film => new FilmViewModel()
            {
                Description = film.Description,
                Director = film.Director,
                FilmId = film.FilmId,
                Name = film.Name,
                Poster = ConvertByteToStringImg(film.Poster),
                Publisher = film.Publisher,
                Year = film.Year
            });
        }

        public static string ConvertByteToStringImg(byte[] image)
        {
            var imreBase64Data = Convert.ToBase64String(image);
            var str = $"data:image/png;base64,{imreBase64Data}";

            return str;
        }
    }
}
