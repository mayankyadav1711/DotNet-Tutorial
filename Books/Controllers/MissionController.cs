using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.DataContext;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly BooksContext _context;

        public MissionsController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/Missions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mission>>> GetMissions()
        {
            return await _context.Missions.ToListAsync();
        }

        // GET: api/Missions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mission>> GetMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);

            if (mission == null)
            {
                return NotFound();
            }

            return mission;
        }

        // PUT: api/Missions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMission(int id, MissionDto missionDto)
        {
            if (id != missionDto.Id)
            {
                return BadRequest();
            }

            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
            {
                return NotFound();
            }

            mission.CountryId = missionDto.CountryId;
            mission.CityId = missionDto.CityId;
            mission.MissionDescription = missionDto.MissionDescription;
            mission.TotalSheets = missionDto.TotalSheets;
            mission.StartDate = missionDto.StartDate;
            mission.EndDate = missionDto.EndDate;
            mission.MissionImages = missionDto.MissionImages;
            mission.MissionSkillId = missionDto.MissionSkillId;

            _context.Entry(mission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissionExists(id))
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

        // POST: api/Missions
        [HttpPost]
        public async Task<ActionResult<Mission>> PostMission(MissionDto missionDto)
        {
            Mission mission = new Mission
            {
                CountryId = missionDto.CountryId,
                CityId = missionDto.CityId,
                MissionDescription = missionDto.MissionDescription,
                TotalSheets = missionDto.TotalSheets,
                StartDate = missionDto.StartDate,
                EndDate = missionDto.EndDate,
                MissionImages = missionDto.MissionImages,
                MissionSkillId = missionDto.MissionSkillId
            };

            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMission", new { id = mission.Id }, mission);
        }

        // DELETE: api/Missions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
            {
                return NotFound();
            }

            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MissionExists(int id)
        {
            return _context.Missions.Any(e => e.Id == id);
        }
    }

    public class MissionDto
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string MissionDescription { get; set; }
        public int? TotalSheets { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? MissionImages { get; set; }
        public string? MissionSkillId { get; set; }
        // Add other fields as needed
    }
}
