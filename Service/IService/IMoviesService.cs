using System.Linq.Expressions;

namespace MoviesApiDevCreed.Service.IService
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAll(Expression<Func<Movie, bool>>? predicate=null, params string[]? eagers);
        Task Create(Movie dto);
        Task<Movie> GetGenre(int id);
        void Update(Movie movie);
        void Delete(Movie movie);
        Task<bool> IsValidGenreId(Byte id);
        Task<Movie> GetById(int id, params string[]? eagers);
        //async Task<Movie> Delete(int id);
    }
}
