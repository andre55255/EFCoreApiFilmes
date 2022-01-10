using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : ControllerBase
    {
        private FilmContext _filmContext;
        private IMapper _autoMapper;

        public FilmController(FilmContext filmContext, IMapper autoMapper)
        {
            _filmContext = filmContext;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        public IActionResult NewFilm([FromBody] CreateFilmDto filmDto)
        {
            Film film = _autoMapper.Map<Film>(filmDto);

            _filmContext.Films.Add(film);
            _filmContext.SaveChanges();

            return CreatedAtAction(nameof(GetFilm), new { Id = film.Id }, film);
        }

        [HttpGet]
        public IActionResult GetFilms()
        {
            return Ok(_filmContext.Films);
        }

        [HttpGet("{id}")]
        public IActionResult GetFilm(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            Film film = _filmContext.Films.FirstOrDefault(film => film.Id == id.Value);

            if (film == null) 
                return NotFound();

            /*ReadFilmDto filmDto = new ReadFilmDto
            {
                Title = film.Title,
                Director = film.Director,
                Category = film.Category,
                Duration = film.Duration,
                Id = film.Id
            };*/
            ReadFilmDto filmDto = _autoMapper.Map<ReadFilmDto>(film);

            return Ok(filmDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFilm(int? id, [FromBody] UpdateFilmDto filmDto)
        {
            if (!id.HasValue)
                return BadRequest();

            Film filmUpdate = _filmContext.Films.FirstOrDefault(f => f.Id == id.Value);

            if (filmUpdate == null)
                return NotFound();

            _autoMapper.Map(filmDto, filmUpdate);

            _filmContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFilm(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            Film film = _filmContext.Films.FirstOrDefault(x => x.Id == id.Value);

            if (film == null)
                return NotFound();

            _filmContext.Films.Remove(film);
            _filmContext.SaveChanges();

            return NoContent();
        }
    }
}
