using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Models;
using DataLayer.ViewModels;
using FilmsCatalog.Filters.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceLayer.Helpers;
using ServiceLayer.Service;

namespace FilmsCatalog.Controllers
{
    public class FilmController : Controller
    {
        readonly ILogger<FilmController> m_Logger;
        readonly ApplicationDbContext m_DbDbContext;
        readonly UserManager<User> m_UserManager;

        public FilmController(ILogger<FilmController> logger, ApplicationDbContext dbDbContext, UserManager<User> userManager)
        {
            m_Logger = logger;
            m_DbDbContext = dbDbContext;
            m_UserManager = userManager;
        }

        [Log]
        public async Task<IActionResult> ShowFilmDetails(string guid)
        {
            if (!string.IsNullOrEmpty(guid))
            {
                try
                {
                    var film = await Task.Factory.StartNew(() => m_DbDbContext
                        .FilmList
                        .FindFilm(guid)
                        .First());

                    var user = await m_UserManager.GetUserAsync(HttpContext.User);

                    ViewBag.IsOwner = film.PublisherEmail == user?.Email;

                    return View(film.ToViewModel());
                }
                catch (Exception ex)
                {
                    m_Logger.LogWarning("Фильм не найден. Ошибка - {error}", ex.Message);
                }
            }

            return RedirectToAction("Error", "Error");
        }

        [Log]
        [Authorize]
        [HttpGet]
        public IActionResult AddFilm()

        {
            try
            {
                ViewBag.Errors = new List<ValidationResult>().ToImmutableList();

                return View();
            }
            catch (Exception ex)
            {
                m_Logger.LogWarning("{error}", ex.Message);
            }

            return RedirectToAction("Error", "Error");
        }

        [Log]
        [ValidateModel]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFilm(FilmViewModel filmViewModel)

        {
            //TODO: Добавить валидацию файла.

            try
            {
                var service = new FilmService();
                service.ValidateFilm(filmViewModel);

                if (service.Errors.Any())
                {
                    ViewBag.Errors = service.Errors;

                    return View();
                }
                else
                {
                    var user = await m_UserManager.GetUserAsync(HttpContext.User);

                    var film = filmViewModel.ToModel(user);
                    m_DbDbContext.FilmList.Add(film);

                    await m_DbDbContext.SaveChangesAsync();

                    ViewBag.IsOwner = true;

                    return View("ShowFilmDetails", film.ToViewModel());
                }

            }
            catch (Exception ex)
            {
                m_Logger.LogWarning("Ошибка при добавлении фильма. Ошибка - {error}", ex.Message);
            }

            return RedirectToAction("Error", "Error");
        }

        [Log]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateFilm(string guid)

        {
            try
            {
                var film = await Task.Factory.StartNew(() => m_DbDbContext
                    .FilmList
                    .FindFilm(guid)
                    .First());

                var user = await m_UserManager.GetUserAsync(HttpContext.User);

                if (user.Email == film.PublisherEmail)
                {
                    ViewBag.Errors = new List<ValidationResult>().ToImmutableList();

                    return View(film.ToViewModel());
                }

            }
            catch (Exception ex)
            {
                m_Logger.LogWarning("Ошибка при добавлении фильма. Ошибка - {error}", ex.Message);
            }

            return RedirectToAction("Error", "Error");
        }

        [Log]
        [ValidateModel]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFilm(FilmViewModel filmViewModel)

        {
            //TODO: Добавить валидацию файла.

            try
            {
                var oldFilm = await Task.Factory.StartNew(() => m_DbDbContext.FilmList.First(item => item.Guid == filmViewModel.Guid));
                var user = await m_UserManager.GetUserAsync(HttpContext.User);

                if (user.Email == oldFilm.PublisherEmail)
                {

                    var service = new FilmService();
                    service.ValidateFilm(filmViewModel);

                    if (service.Errors.Any())
                    {
                        ViewBag.Errors = service.Errors;

                        return View();
                    }
                    else
                    {

                        var newFilm = FilmListHelper.UpdateFilm(filmViewModel.ToModel(user), oldFilm);
                        await m_DbDbContext.SaveChangesAsync();

                        ViewBag.IsOwner = true;

                        return View("ShowFilmDetails", newFilm.ToViewModel());
                    }
                }

            }
            catch (Exception ex)
            {
                m_Logger.LogWarning("Ошибка при обновлении фильма. Ошибка - {error}", ex.Message);
            }

            return RedirectToAction("Error", "Error");
        }

    }
}