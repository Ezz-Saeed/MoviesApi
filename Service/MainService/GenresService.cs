using MoviesApiDevCreed.DTOs;
using MoviesApiDevCreed.Service.IService;
using MoviesApiDevCreed.UnitOfWork;
using System.Linq.Expressions;

namespace MoviesApiDevCreed.Service.MainService
{
    public class GenresService: IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GenresService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Genre dto)
        {
            await _unitOfWork.Genres.Create(dto);
        }

        public void Delete(Genre genre)
        {
            _unitOfWork.Genres.Delete(genre);
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
           return await _unitOfWork.Genres.GetAll(null);
        }

        public async Task<Genre> GetGenre(int id)
        {
            return await _unitOfWork.Genres.GetByIdWithEagers(g => g.Id == id);
        }

        public void Update(Genre genre)
        {
            _unitOfWork.Genres.Update(genre);
        }
    }
}
