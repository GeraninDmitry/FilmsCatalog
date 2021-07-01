using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DataLayer.ViewModels
{
    public class FilmViewModel
    {
        public string Guid { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Название фильма")]
        [StringLength(200, ErrorMessage = "Название фильма должено содержать минимум {2} и максимум {1} символов", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Описание")]
        [StringLength(1000, ErrorMessage = "Описание должено содержать минимум {2} и максимум {1} символов", MinimumLength = 30)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Год выпуска")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Режиссёр")]
        [StringLength(200, ErrorMessage = "Имя режиссёра должено содержать минимум {2} и максимум {1} символов", MinimumLength = 1)]
        public string Director { get; set; }

        public string Publisher { get; set; }

        public string Poster { get; set; }

        [Display(Name = "Постер")]
        public IFormFile RawPoster { get; set; }
    }
}
