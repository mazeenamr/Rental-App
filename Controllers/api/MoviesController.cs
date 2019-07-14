using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vidly.Dtos;
using vidly.Models;

namespace vidly.Controllers.api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _Context;

        public MoviesController() 
        {
            _Context = new ApplicationDbContext();
        }
        public IEnumerable<movieDtos> GetMovies()
        {
            return _Context.Movies
                .Include(c => c.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, movieDtos>);
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _Context.Movies.SingleOrDefault(c => c.id == id);
            if (movie == null)
                return NotFound();
            return Ok(Mapper.Map<Movie, movieDtos>(movie));
        }
        [HttpPost]
        public IHttpActionResult CreateMovie(movieDtos moviesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movie = Mapper.Map<movieDtos, Movie>(moviesDto);
            _Context.Movies.Add(movie);
            _Context.SaveChanges();

            moviesDto.id = movie.id;
            return Created(new Uri(Request.RequestUri + "/" + movie.id), moviesDto);

        }
        [HttpPut]
        public void UpdateMovie(int id, movieDtos moviesDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var moviesInDb = _Context.Movies.SingleOrDefault(c => c.id == id);
            if (moviesInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(moviesDto, moviesInDb);
            
            _Context.SaveChanges();

        }
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var moviesInDb = _Context.Movies.SingleOrDefault(c => c.id == id);
            if (moviesInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _Context.Movies.Remove(moviesInDb);
            _Context.SaveChanges();

        }
    }
}
