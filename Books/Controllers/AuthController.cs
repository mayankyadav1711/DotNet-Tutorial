using Books.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Books.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;  //Injected configuration object (issuer,audience and key) for accessing application settings.
        private readonly BooksContext _context; // Injected database context for interacting with the data store.

        //passing both of them in the below constructor
        public AuthController(IConfiguration config, BooksContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous] //attribute allow anonymous user to access this action
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user); //generates the JWT token
                return Ok(new { Message = "Welcome " + user.GivenName, Token = token });
            }

            return NotFound("User not found");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username.ToLower() == user.Username.ToLower());
            if (existingUser != null)
            {
                return BadRequest("Username already exists");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User registered successfully");
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            // Using LINQ to query the Users table
            var users = _context.Users
                .Select(u => new
                {
                    Username = u.Username,
                    Email = u.EmailAddress,
                    FullName = $"{u.GivenName} {u.Surname}"
                })
                .ToList();

            return Ok(users);
        }


        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // information about the users 
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],  // who issued the token (Our Server)
              _config["Jwt:Audience"], // who the token is intended for (Client) 
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(UserLogin userLogin)
        {
            var currentUser = _context.Users.FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
