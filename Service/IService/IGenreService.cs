using MoviesApiDevCreed.DTOs;
using System.Linq.Expressions;

namespace MoviesApiDevCreed.Service.IService
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task Create(Genre dto);
        Task<Genre> GetGenre(int id);
        void Update(Genre genre);
        void Delete(Genre genre);
    }
}
