using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmsCatalog.Filters.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FilmsCatalog.Controllers
{
    public class ErrorController : Controller
    {

        public ErrorController() { }

        [Log]
        public IActionResult Error()
        {
            return View();
        }


    }
}
