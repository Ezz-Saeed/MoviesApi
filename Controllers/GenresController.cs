using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApiDevCreed.DTOs;
using MoviesApiDevCreed.Service.IService;
using MoviesApiDevCreed.Service.MainService;

namespace MoviesApiDevCreed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genresService;
        public GenresController(IGenreService genresService)
        {
            _genresService = genresService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genres = await _genresService.GetAll();
            return Ok(genres);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGenreDto dto)
        {
            Genre genre = new() { Name = dto.Name };
            await _genresService.Create(genre);
            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateGenreDto dto)
        {
            Genre genre = await _genresService.GetGenre(id);
            if(genre is null)
                return NotFound($"No genre with id {id}");
            genre.Name = dto.Name;
            _genresService.Update(genre);
            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Genre genre = await _genresService.GetGenre(id);
            if (genre is null)
                return NotFound($"No genre with id {id}");
            _genresService.Delete(genre);
            return Ok(genre);
        }

    }
}
