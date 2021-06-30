using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.Extensions
{
    public static class FileListExtensions
    {
        public static IQueryable<FilmViewModel> CreatePage(this IQueryable<Film> list, ApplicationDbContext dbDbContext, int page, int pageSize)
        {
            var query = dbDbContext
				.FilmList
                .AsNoTracking()
                .OrderBy(film => film.PublishDate)
                .Page(page, pageSize)
                .ToViewModel();

            return query;
        }

        public static IQueryable<FilmViewModel> FindFilm(this IQueryable<Film> list, ApplicationDbContext dbDbContext, string guid)
        {
            var query = dbDbContext
				.FilmList
                .Where(item => item.Guid == guid)
                .ToViewModel();

            return query;
        }

        static IQueryable<Film> Page(this IQueryable<Film> list, int page, int pageSize)
        {
            list = list.Skip((page - 1) * pageSize);

            var items = list.Take(pageSize);

            return items;
        }

		public static IQueryable<FilmViewModel> ToViewModel(this IQueryable<Film> list)
        {
            return list.Select(film => new FilmViewModel()
            {
                Description = film.Description,
                Director = film.Director,
                Guid = film.Guid,
                Name = film.Name,
                Poster = ConvertByteToStringImg(film.Poster),
                Publisher = film.Publisher,
                Year = film.Year
            });
        }

        static string ConvertByteToStringImg(byte[] image)
        {
            var imreBase64Data = Convert.ToBase64String(image);
            var str = $"data:image/png;base64,{imreBase64Data}";

            return str;
        }
    }
}
