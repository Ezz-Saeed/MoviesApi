namespace MoviesApiDevCreed.DTOs
{
    public class UpdateMovieDto : MovieDto
    {
        public IFormFile? Poster { get; set; }
    }
}
