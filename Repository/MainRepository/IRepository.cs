using System.Linq.Expressions;

namespace MoviesApiDevCreed.Repository.MainRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? predicate, params string[]? eagers);
        Task Create(T entity);
        Task<T> GetByIdWithEagers(Expression<Func<T, bool>> predicate, params string[]? eagers);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetById(byte id);
    }
}
