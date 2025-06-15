using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_webapi.Core.Interfaces;
using test_webapi.Models;

namespace test_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Authors        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return Ok(authors);
        }

        // GET: api/Authors/5        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // POST: api/Authors        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        // PUT: api/Authors/5        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            await _unitOfWork.Authors.UpdateAsync(author);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AuthorExists(id))
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

        // DELETE: api/Authors/5        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            await _unitOfWork.Authors.DeleteAsync(author);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _unitOfWork.Authors.ExistsAsync(id);
        }
    }
}
