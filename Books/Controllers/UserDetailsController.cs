using Books.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly BooksContext _context;

        public UserDetailsController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/v1/UserDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetUserDetails()
        {
            return await _context.UserDetails.ToListAsync();
        }

        // GET: api/v1/UserDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetail>> GetUserDetail(int id)
        {
            var userDetail = await _context.UserDetails.FindAsync(id);

            if (userDetail == null)
            {
                return NotFound();
            }

            return userDetail;
        }

        // PUT: api/v1/UserDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDetail(int id, UserDetailDto userDetailDto)
        {
            if (id != userDetailDto.Id)
            {
                return BadRequest();
            }

            var userDetail = await _context.UserDetails.FindAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            userDetail.UserId = userDetailDto.UserId;
            userDetail.Name = userDetailDto.Name;
            userDetail.Surname = userDetailDto.Surname;
            userDetail.EmployeeId = userDetailDto.EmployeeId;
            userDetail.Manager = userDetailDto.Manager;
            userDetail.Title = userDetailDto.Title;
            userDetail.Department = userDetailDto.Department;
            userDetail.MyProfile = userDetailDto.MyProfile;
            userDetail.WhyIVolunteer = userDetailDto.WhyIVolunteer;
            userDetail.CountryId = userDetailDto.CountryId;
            userDetail.CityId = userDetailDto.CityId;
            userDetail.Availability = userDetailDto.Availability;
            userDetail.LinkdInUrl = userDetailDto.LinkdInUrl;
            userDetail.MySkills = userDetailDto.MySkills;
            userDetail.UserImage = userDetailDto.UserImage;
            userDetail.Status = userDetailDto.Status;

            _context.Entry(userDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailExists(id))
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

        // POST: api/v1/UserDetails
        [HttpPost]
        public async Task<ActionResult<UserDetail>> PostUserDetail(UserDetailDto userDetailDto)
        {
            var userDetail = new UserDetail
            {
                UserId = userDetailDto.UserId,
                Name = userDetailDto.Name,
                Surname = userDetailDto.Surname,
                EmployeeId = userDetailDto.EmployeeId,
                Manager = userDetailDto.Manager,
                Title = userDetailDto.Title,
                Department = userDetailDto.Department,
                MyProfile = userDetailDto.MyProfile,
                WhyIVolunteer = userDetailDto.WhyIVolunteer,
                CountryId = userDetailDto.CountryId,
                CityId = userDetailDto.CityId,
                Availability = userDetailDto.Availability,
                LinkdInUrl = userDetailDto.LinkdInUrl,
                MySkills = userDetailDto.MySkills,
                UserImage = userDetailDto.UserImage,
                Status = userDetailDto.Status
            };

            _context.UserDetails.Add(userDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDetail", new { id = userDetail.Id }, userDetail);
        }

        // DELETE: api/v1/UserDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDetail(int id)
        {
            var userDetail = await _context.UserDetails.FindAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            _context.UserDetails.Remove(userDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDetailExists(int id)
        {
            return _context.UserDetails.Any(e => e.Id == id);
        }
    }

    public class UserDetailDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmployeeId { get; set; }
        public string Manager { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string MyProfile { get; set; }
        public string WhyIVolunteer { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Availability { get; set; }
        public string LinkdInUrl { get; set; }
        public string MySkills { get; set; }
        public string UserImage { get; set; }
        public bool Status { get; set; }
    }
}