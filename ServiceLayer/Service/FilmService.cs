using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BizLogic;
using DataLayer.ViewModels;
using ServiceLayer.BizRunners;

namespace ServiceLayer.Service
{
    public class FilmService
    {
        readonly SimpleBizRunner<FilmViewModel> m_BizRunner;

        public IImmutableList<ValidationResult> Errors => m_BizRunner.Errors;

        public FilmService()
        {
            m_BizRunner = new SimpleBizRunner<FilmViewModel>(new ValidateFilmAction());
        }

        public void ValidateFilm(FilmViewModel film)
        {
            m_BizRunner.RunAction(film);
        }
    }
}
