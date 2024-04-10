namespace MoviesApiDevCreed.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Genre> Genres { get; }
        IRepository<Movie> Movies { get; }
    }
}
