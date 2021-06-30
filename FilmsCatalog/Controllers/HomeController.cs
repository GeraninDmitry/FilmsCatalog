using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.ViewModels;
using FilmsCatalog.Filters.Actions;
using FilmsCatalog.Helpers.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Extensions;

namespace FilmsCatalog.Controllers
{
    public class HomeController : Controller
    {

        readonly ILogger<HomeController> m_Logger;
        readonly ApplicationDbContext m_DbDbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            m_Logger = logger;
            m_DbDbContext = dbContext;
        }

        [Log]
        public async Task<IActionResult> Index(int selectedPage)
        {
            try
            {
                var filmsAmount = await Task.Factory.StartNew(() => m_DbDbContext.FilmList.
                    AsNoTracking().
                    Count());

                var paginationHelper = new PaginationHelper(filmsAmount);
                var validatedPage = paginationHelper.ValidateSelectedPage(selectedPage);
                var paginationType = paginationHelper.DeterminePaginationType(validatedPage);

                var filmsPage = await Task.Factory.StartNew(() => m_DbDbContext.FilmList
                    .CreatePage(m_DbDbContext, validatedPage, PaginationHelper.PageSize)
                    .ToList());

                ViewBag.PagesAmount = paginationHelper.PagesAmount;
                ViewBag.PaginationType = paginationType;
                ViewBag.SelectedPage = validatedPage;

                return View(filmsPage);
            }
            catch (Exception ex)
            {
                m_Logger.LogError("Ошибка - {Exception}", ex.Message);
            }

            return RedirectToAction("Error", "Error");
        }

    }
}
