using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class FilmViewModel
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public string Publisher { get; set; }
        public string Poster { get; set; }
    }
}
