using test_webapi.Core.Interfaces;
using test_webapi.Data;
using test_webapi.Models;

namespace test_webapi.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;
        private IGenericRepository<Book>? _books;
        private IGenericRepository<Author>? _authors;
        private IGenericRepository<Category>? _categories;

        public UnitOfWork(LibraryContext context)
        {
            _context = context;
        }

        public IGenericRepository<Book> Books =>
            _books ??= new GenericRepository<Book>(_context);

        public IGenericRepository<Author> Authors =>
            _authors ??= new GenericRepository<Author>(_context);

        public IGenericRepository<Category> Categories =>
            _categories ??= new GenericRepository<Category>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
