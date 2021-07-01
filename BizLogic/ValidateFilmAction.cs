using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizLogic.GenericInterfaces;
using DataLayer.Models;
using DataLayer.ViewModels;

namespace BizLogic
{
    public class ValidateFilmAction : BizActionErrors, ISimpleBizAction<FilmViewModel>
    {
        public ValidateFilmAction() { }


        public void Action(FilmViewModel film)
        {
            if (film.Year <= 1895 || film.Year > DateTime.Now.Year)
            {
                AddError("Неверно указан год выпуска фильма");
            }

            if (film.Description.Length < 30)
            {
                AddError("Слишком короткое описание ");
            }

            if (film.Description.Length > 1000)
            {
                AddError("Слишком длинное описание ");
            }

            if (film.Name.Length > 200)
            {
                AddError("Слишком длинное название фильма");
            }

            if (film.Director.Length > 200)
            {
                AddError("Слишком длинное имя режиссёра");
            }

        }
    }
}
