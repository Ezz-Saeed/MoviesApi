using MoviesApiDevCreed.Repository.ModelRepositories;

namespace MoviesApiDevCreed.UnitOfWork
{
    public class MyUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public MyUnitOfWork(AppDbContext context)
        {
            _context = context;
           Genres = new MainRepository<Genre>(context);
            Movies = new MainRepository<Movie>(context);
        }

        public IRepository<Genre> Genres{get; private set;}
        public IRepository<Movie> Movies { get; private set;}
    }
}
