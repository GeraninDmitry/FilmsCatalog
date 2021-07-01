using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Models;
using FilmsCatalog.Filters.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceLayer.Helpers;

namespace FilmsCatalog.Controllers
{
    public class FavoritesController : Controller
    {
        readonly ILogger<FavoritesController> m_Logger;
        readonly ApplicationDbContext m_DbDbContext;
        readonly UserManager<User> m_UserManager;

        public FavoritesController(ILogger<FavoritesController> logger, ApplicationDbContext dbDbContext,
			UserManager<User> userManager)
        {
            m_Logger = logger;
            m_DbDbContext = dbDbContext;
			m_UserManager = userManager;
		}

        [Log]
        [Authorize]
        public async Task<IActionResult> ShowFavorites()
        {
            var user = await m_UserManager.GetUserAsync(HttpContext.User);

            var filmList = await Task.Factory.StartNew(() => m_DbDbContext
                .FilmList
                .GetUsersFilms(user)
                .ToList());

            return View(filmList);
        }

    }
}
