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
        public static IQueryable<FilmViewModel> CreatePage(this IQueryable<Film> list, ApplicationDbContext m_DbDbContext, int page, int pageSize)
        {
            var query = m_DbDbContext.FilmList
                .AsNoTracking()
                .OrderBy(film => film.PublishDate)
                .Page(page, pageSize)
                .ToViewModel();

            return query;
        }

        static IQueryable<Film> Page(this IQueryable<Film> list, int page, int pageSize)
        {
            list = list.Skip((page - 1) * pageSize);

            var items = list.Take(pageSize);

            return items;
        }

        static IQueryable<FilmViewModel> ToViewModel(this IQueryable<Film> list)
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

        static string ConvertByteToStringImg(byte[] image)
        {
            var imreBase64Data = Convert.ToBase64String(image);
            var str = $"data:image/png;base64,{imreBase64Data}";

            return str;
        }
    }
}
