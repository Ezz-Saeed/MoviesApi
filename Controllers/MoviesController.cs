using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApiDevCreed.DTOs;
using MoviesApiDevCreed.Service.IService;

namespace MoviesApiDevCreed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly List<string> AllowedExtensions = new() { ".jpg", ".png" };
        private readonly long MaxSize = 1048576;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet]
        public async Task <IActionResult> GetAllMovies()
        {
            var movies = await _moviesService.GetAll(eagers:"Genre");
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _moviesService.GetById(id, "Genre");
            if (movie is null)
                return NotFound();
            return Ok(movie);
        }

        [HttpGet("GetbyGenreId")]
        public async Task<IActionResult> GetbyGenreId(byte id)
        {
            var movies = await _moviesService.GetAll(m => m.GenreId == id, "Genre");
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateMovieDto dto)
        {
            if (!AllowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName)))
                return BadRequest("Invalid poster extension.");
            if(dto.Poster.Length > MaxSize)
                return BadRequest("Out of range poster size.");
            if (!await _moviesService.IsValidGenreId(dto.GenreId)) 
                return BadRequest("Invalid genre ID!");
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            var movie = new Movie()
            {
                Title = dto.Title,
                Year = dto.Year,
                Rate = dto.Rate,
                StoryLine = dto.StoryLine,
                Poster = dataStream.ToArray(),
                GenreId = dto.GenreId,
            };

            await _moviesService.Create(movie);
            return Ok(movie);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromForm] UpdateMovieDto dto)
        {
            var movie = await _moviesService.GetById(id);
            if (movie is null)
                return NotFound($"No movie with id {id}");
            if(dto.Poster is not null && dto.Poster.Length > 0)
            {
                if (!AllowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName)))
                    return BadRequest("Invalid poster extension.");
                if (dto.Poster.Length > MaxSize)
                    return BadRequest("Out of range poster size.");                
                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
                movie.Poster = dataStream.ToArray();
            }
            if (!await _moviesService.IsValidGenreId(dto.GenreId))
                return BadRequest("Invalid genre ID!");
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Rate = dto.Rate;
            movie.StoryLine = dto.StoryLine;
            movie.GenreId = dto.GenreId;

             _moviesService.Update(movie);
            return Ok(movie);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _moviesService.GetById(id);
            if (movie is null)
                return NotFound($"No movie with id {id}");
            _moviesService.Delete(movie);
            return Ok(movie);
        }
    }
}
