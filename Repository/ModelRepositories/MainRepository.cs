


using MoviesApiDevCreed.Repository.MainRepository;
using System.Linq.Expressions;

namespace MoviesApiDevCreed.Repository.ModelRepositories
{
    public class MainRepository<T> : IRepository<T> where T:class 
    {
        private readonly AppDbContext _context;
        public MainRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task Create(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
           _context.SaveChanges();
        }

        public void Delete(T entity)
        {
             _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? predicate, params string[]? eagers)
        {
            IQueryable<T> values;
            if (predicate is not null)
                values = _context.Set<T>().Where(predicate);
            else
                values = _context.Set<T>();
            if(eagers is not null && eagers.Length > 0)
            {
                foreach(var eager in eagers)
                    values = values.Include(eager);
            }
            return await values.ToListAsync();
        }

        public async Task<T> GetByIdWithEagers(Expression<Func<T, bool>> predicate, params string[]? eagers)
        {
            IQueryable<T> values = _context.Set<T>();
            if (eagers is not null && eagers.Length > 0)
            {
                foreach (var eager in eagers)
                    values = values.Include(eager);
            }
            var targetvalue = await values.SingleOrDefaultAsync(predicate);
            return targetvalue!;
        }

        public void Update(T entity)
        {
             _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public async Task<T> GetById(byte id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        


    }
}
