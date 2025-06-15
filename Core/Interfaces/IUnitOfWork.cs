namespace test_webapi.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Models.Book> Books { get; }
        IGenericRepository<Models.Author> Authors { get; }
        IGenericRepository<Models.Category> Categories { get; }

        Task<int> CompleteAsync();
    }
}
