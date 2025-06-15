using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_webapi.Core.Interfaces;
using test_webapi.Models;

namespace test_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Books        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return Ok(books);
        }

        // GET: api/Books/5        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // POST: api/Books        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // PUT: api/Books/5        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            await _unitOfWork.Books.UpdateAsync(book);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Books/5        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await _unitOfWork.Books.DeleteAsync(book);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        private async Task<bool> BookExists(int id)
        {
            return await _unitOfWork.Books.ExistsAsync(id);
        }
    }
}
