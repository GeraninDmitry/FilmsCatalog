using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmsCatalog.Helpers.Enums;

namespace FilmsCatalog.Helpers.Controllers
{
    public class PaginationHelper
    {
        public const int PageSize = 15;
        public const int PaginationTypeToggle = 4;
        public const int SimplePageAmount = 10;

        public readonly int PagesAmount;

        public PaginationHelper(int itemsAmount)
        {
            PagesAmount = (int)Math.Ceiling(Convert.ToDouble(itemsAmount) / Convert.ToDouble(PageSize));
        }

        public PaginationType DeterminePaginationType(int selectedPage)
        {
            var paginationType = PaginationType.Start;

            if (PagesAmount < SimplePageAmount)
            {
                paginationType = PaginationType.Simple;
            }
            else if (selectedPage < PaginationTypeToggle)
            {
                paginationType = PaginationType.Start;
            }
            else if (PagesAmount - selectedPage <= PaginationTypeToggle)
            {
                paginationType = PaginationType.End;
            }
            else
            {
                paginationType = PaginationType.Middle;
            }

            return paginationType;
        }

        public int ValidateSelectedPage(int selectedPage)
        {
            if (selectedPage <= 0)
            {
                selectedPage = 1;
            }
            else if (selectedPage > PagesAmount)
            {
                selectedPage = (int)PagesAmount;
            }

            return selectedPage;
        }
    }
}
