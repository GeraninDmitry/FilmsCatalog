using System;
using System.IO;
using System.Linq;
using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.Helpers
{
    public static class FilmListHelper
    {
        public static IQueryable<FilmViewModel> CreatePage(this IQueryable<Film> list, int page, int pageSize)
        {
            var query = list
                .AsNoTracking()
                .OrderByDescending(film => film.PublishDate)
                .Page(page, pageSize)
                .ToViewModel();

            return query;
        }

        public static IQueryable<Film> FindFilm(this IQueryable<Film> list, string guid)
        {
            var query = list
                .AsNoTracking()
                .Where(item => item.Guid == guid);

            return query;
        }

        public static IQueryable<FilmViewModel> GetUsersFilms(this IQueryable<Film> list, User user)
        {
            var query = list
                .AsNoTracking()
                .Where(item => item.PublisherEmail == user.Email)
                .OrderByDescending(film => film.PublishDate)
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
                Poster = ConvertByteToString(film.Poster),
                Publisher = film.Publisher,
                Year = film.Year
            });
        }

        public static Film ToModel(this FilmViewModel filmViewModel, User user)
        {
            return new Film()
            {
                Guid = Guid.NewGuid().ToString(),
                Director = filmViewModel.Director,
                Poster = ConvertStringToByteImg(filmViewModel.RawPoster),
                Name = filmViewModel.Name,
                Year = filmViewModel.Year,
                Publisher = $"{user.LastName} {user.FirstName} {user.MiddleName}",
                PublisherEmail = user.Email,
                Description = filmViewModel.Description,
                PublishDate = DateTime.Now
            };
        }

        public static FilmViewModel ToViewModel(this Film film)
        {
            return new FilmViewModel()
            {
                Description = film.Description,
                Director = film.Director,
                Guid = film.Guid,
                Name = film.Name,
                Poster = ConvertByteToString(film.Poster),
                Publisher = film.Publisher,
                Year = film.Year
            };
        }

        static string ConvertByteToString(byte[] image)
        {
            string str = null;

            if (image != null)
            {
                var imreBase64Data = Convert.ToBase64String(image);
                str = $"data:image/png;base64,{imreBase64Data}";
            }

            return str;
        }

        static byte[] ConvertStringToByteImg(IFormFile image)
        {
            byte[] imageData = null;

            if (image != null)
            {
                using (var binaryReader = new BinaryReader(image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)image.Length);
                }
            }

            return imageData;
        }

        public static Film UpdateFilm(Film newFilm, Film oldFilm)
        {
			if (IsNewPoster(oldFilm, newFilm))
            {
                oldFilm.Poster = newFilm.Poster;
            }

            if (oldFilm.Description != newFilm.Description)
            {
                oldFilm.Description = newFilm.Description;
            }

            if (oldFilm.Director != newFilm.Director)
            {
                oldFilm.Director = newFilm.Director;
            }

            if (oldFilm.Name != newFilm.Name)
            {
                oldFilm.Name = newFilm.Name;
            }

            if (oldFilm.Year != newFilm.Year)
            {
                oldFilm.Year = newFilm.Year;
            }

			return oldFilm;
		}

        static bool IsNewPoster(Film oldFilm, Film newFilm)
        {
            return newFilm.Poster != null
                && ((oldFilm.Poster != null && !oldFilm.Poster.SequenceEqual(newFilm.Poster))
                    || oldFilm.Poster == null);
        }
    }
}
