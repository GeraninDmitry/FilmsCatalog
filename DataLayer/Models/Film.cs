using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Film
    {
        public int FilmId { get; set; }
		public string Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public string Publisher { get; set; }
        public byte[] Poster { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
