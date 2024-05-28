using Microsoft.EntityFrameworkCore;

namespace Books.DataContext
{
    //DbContext is the primary class for interacting with database
    public class BooksContext : DbContext  //DbContext act as a bridge between our class and db 
    {
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
            // DbContextOptions<BooksContext> used to configure the context 
            // base(options) yeh saare options base class (DbContext) ko bhej deta h 
        }

        //DbSet is a class representing collection of entities. (here Book)
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }

    }
}
