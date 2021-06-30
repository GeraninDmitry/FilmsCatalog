using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Models;
using DataLayer.ViewModels;
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
                        if (context.Database.EnsureCreated())
                        {
                            FillFilmList(context);

                            context.SaveChanges();
                        }

                    }
                    catch (Exception ex)
                    {
                        // TODO: добавить лог.
                    }
                }
            }

            return webHost;
        }

        static void FillFilmList(ApplicationDbContext context)
        {
            for (var i = 0; i < 200; i++)
            {
                context.FilmList.Add(new Film()
                {
                    Director = "Жан-Жак Анно",
                    Poster = File.ReadAllBytes($"{GetWwwRootPath()}/img/posters/poster.jpg"),
                    Description =
                        "Фильм снят по мотивам одноимённой автобиографической книги Генриха Харрера, описывающей историю приключений австрийского альпиниста в Тибете в годы Второй мировой войны. В фильме сохранена только общая последовательность событий, многие подробности придуманы.",
                    Year = 1997,
                    Publisher = "Default",
                    Name = "Семь лет в Тибете",
                    PublishDate = DateTime.Now
                });
            }
        }

        public static IWebHost AddDefaultUser(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                using var userManager = services.GetRequiredService<UserManager<User>>();
                using var context = services.GetRequiredService<ApplicationDbContext>();

                try
                {
                    var user = new User
                    {
                        UserName = "user@mail.ru",
                        Email = "user@mail.ru",
                        FirstName = "user",
                        MiddleName = "user",
                        LastName = "user"
                    };

                    var task = userManager.CreateAsync(user, "qertY1234!");
                    task.Wait();

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    // TODO: добавить лог.
                }
            }

            return webHost;
        }


        public static string GetWwwRootPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), WwwRootDirectory);
        }


    }
}
