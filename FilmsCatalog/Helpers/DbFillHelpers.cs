using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FilmsCatalog.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FilmsCatalog.Helpers
{
    public static class DbFillHelpers
    {
        const string WwwRootDirectory = "wwwroot//";

        public static IWebHost FillDb(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                using (var context = services.GetRequiredService<ApplicationDbContext>())
                {
                    try
                    {
                        context.Database.EnsureCreated();

                        //TODO: Добавить заполнение db.

                    }
                    catch (Exception ex)
                    {
                        // TODO: добавить лог.
                    }
                }
            }

            return webHost;
        }


    }
}
