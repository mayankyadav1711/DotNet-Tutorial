using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Books.Controllers
{
    // Define the route for the API controller and specify that it is an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        // Static list to hold Book objects
        static List<Book> books = new List<Book>();

        // HTTP GET method to retrieve all books
        // GET: api/<BookController>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return books; // Return the list of books
        }

        // HTTP GET method to retrieve a book by ID
        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            // Find and return the book with the specified ID, or null if not found
            return books.FirstOrDefault(s => s.Id == id);
        }

        // HTTP POST method to add a new book
        // POST api/<BookController>
        [HttpPost]
        public void Post([FromBody] Book value)
        {
            // Add the new book to the list
            books.Add(value);
        }

        // HTTP PUT method to update an existing book by ID
        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Book value)
        {
            // Find the index of the book with the specified ID
            int i = books.FindIndex(s => s.Id == id);
            if (i > 0) // If the book is found (index is non-negative)
            {
                books[i] = value; // Update the book at the found index
            }
        }

        // HTTP DELETE method to delete a book by ID
        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // Remove all books with the specified ID
            books.RemoveAll(s => s.Id == id);
        }
    }
}
