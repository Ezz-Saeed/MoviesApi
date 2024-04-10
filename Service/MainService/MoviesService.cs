using MoviesApiDevCreed.Service.IService;
using MoviesApiDevCreed.UnitOfWork;
using System.Linq.Expressions;

namespace MoviesApiDevCreed.Service.MainService
{
    public class MoviesService : IMoviesService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MoviesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Movie dto)
        {
            await _unitOfWork.Movies.Create(dto);
        }

        public void Delete(Movie movie)
        {
            _unitOfWork.Movies.Delete(movie);
        }

        public async Task<IEnumerable<Movie>> GetAll(Expression<Func<Movie, bool>>? predicate=null, params string[]? eagers)
        {
            return await _unitOfWork.Movies.GetAll(predicate,eagers);
        }

        public async Task<Movie> GetGenre(int id)
        {
            return await _unitOfWork.Movies.GetByIdWithEagers(g => g.Id == id);
        }

        public void Update(Movie movie)
        {
            _unitOfWork.Movies.Update(movie);
        }

        public async Task<bool> IsValidGenreId(Byte id)
        {
            var validId = await _unitOfWork.Genres.GetById(id);
            return validId is not null? true : false;
        }

        public async Task<Movie> GetById(int id, params string[]? eagers)
        {
            return await _unitOfWork.Movies.GetByIdWithEagers(m => m.Id == id, eagers);
        }


    }
}
