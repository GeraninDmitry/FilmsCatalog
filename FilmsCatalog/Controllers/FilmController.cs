using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Data;
using FilmsCatalog.Filters.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ServiceLayer.Extensions;

public class FilmController : Controller
{
    readonly ILogger<FilmController> m_Logger;
    readonly ApplicationDbContext m_DbDbContext;

    public FilmController(ILogger<FilmController> logger, ApplicationDbContext dbDbContext)
    {
        m_Logger = logger;
        m_DbDbContext = dbDbContext;
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
					.FindFilm(m_DbDbContext, guid)
                    .First());

                return View(film);
            }
            catch (Exception ex)
            {
                m_Logger.LogWarning("Фильм не найден. Ошибка - {error}", ex.Message);
            }
        }

        return RedirectToAction("Error", "Error");
    }

}