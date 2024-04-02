using BookLibrary.Models;
using BookLibrary.WorkerContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly WorkerDbContext _workerDbContext;
        private readonly ILogger<WeatherForecastController> _logger;

        public BookController(WorkerDbContext workerDbContext, ILogger<WeatherForecastController> logger)
        {
            _workerDbContext = workerDbContext;
            _logger = logger;
        }

        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks(string? author, string? isbn, string? status)
        {
            var query = _workerDbContext.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(b => (b.First_Name + " " + b.Last_Name).Contains(author));
            }

            if (!string.IsNullOrWhiteSpace(isbn))
            {
                query = query.Where(b => b.ISBN == isbn);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(b => b.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            return await query.ToListAsync();
        }

        [HttpGet]
        [Route("GetBook")]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _workerDbContext.Books.ToListAsync();
        }

        [HttpPost]
        [Route("AddBook")]
        public async Task<Book> AddBook(Book book)
        {
            _workerDbContext.Books.Add(book);
             await _workerDbContext.SaveChangesAsync();
            return book;
        }

        [HttpPatch]
        [Route("UpdateBook/{id}")]
        public async Task<Book> UpdateBook(Book book)
        {
            _workerDbContext.Entry(book).State = EntityState.Modified;
            await _workerDbContext.SaveChangesAsync();
            return book;
        }

        [HttpDelete]
        [Route("DeleteBook/{id}")]
        public bool DeleteBook(int id)
        {
            bool response = false;
            var book = _workerDbContext.Books.Find(id);
            if(book != null)
            {
                response = true;
                _workerDbContext.Entry(book).State = EntityState.Deleted;
                _workerDbContext.SaveChanges();
            }
            else
            {
                response = false;
            }

            return response;

        }

    }
}
