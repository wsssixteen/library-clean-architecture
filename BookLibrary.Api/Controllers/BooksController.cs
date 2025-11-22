using BookLibrary.Application.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _service;

        public BooksController(BookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAll(CancellationToken ct)
        {
            var books = await _service.GetAllAsync(ct);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDto>> GetById(int id, CancellationToken ct)
        {
            var book = await _service.GetByIdAsync(id, ct);
            if (book is null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> Create([FromBody] BookCreateRequest request, CancellationToken ct)
        {
            var created = await _service.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateRequest request, CancellationToken ct)
        {
            if (id != request.Id)
                return BadRequest("Route ID and body ID must match");

            var updated = await _service.UpdateAsync(request, ct);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var deleted = await _service.DeleteAsync(id, ct);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

// More concise controller
// Business logic is in BookService (Application layer)
// Persistence details live in BookRepository & LibraryDbContext (Infrastructure)
// Entity rules live in Book (Domain)
// Clean & SOLID code