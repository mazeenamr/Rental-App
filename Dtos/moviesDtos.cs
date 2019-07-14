using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidly.Dtos
{
    public class movieDtos

    {
        public int id { get; set; }
        public string Name { get; set; }
        public GenreDtos Genre { get; set; }
        public int GenreId { get; set; }
        public string ReleaseDate { get; set; }

        public string DateAdded { get; set; }
        public int NumberInStock { get; set; }

    }
}