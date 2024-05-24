using Books.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Books
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create a new WebApplicationBuilder instance
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Add controllers to the service collection
            builder.Services.AddControllers();

            // Add support for API endpoint exploration and documentation generation
            // with Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure PostgreSQL
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BooksContext>(options =>
                options.UseNpgsql(connectionString));

            // Build the WebApplication instance
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // If the environment is development, enable Swagger and Swagger UI
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();

            // Enable authorization middleware (typically configured later)
            app.UseAuthorization();

            // Map controllers to endpoints
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}
