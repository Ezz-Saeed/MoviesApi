namespace MoviesApiDevCreed.DTOs
{
    public class CreateMovieDto : MovieDto
    {
        public IFormFile Poster { get; set; }
    }
}
