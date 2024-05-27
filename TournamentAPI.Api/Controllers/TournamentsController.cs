using Microsoft.AspNetCore.Mvc;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;

namespace TournamentAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TournamentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
        {
            return (await _unitOfWork.TournamentRepository.GetAllAsync()).ToList();
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            return tournament;
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return BadRequest();
            }

            var getTournament = await _unitOfWork.TournamentRepository.GetAsync(id);

            if (getTournament == null)
                return NotFound();

            _unitOfWork.TournamentRepository.Update(tournament);

            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            _unitOfWork.TournamentRepository.Add(tournament);
            return CreatedAtAction(nameof(GetTournament), new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var result = await _unitOfWork.TournamentRepository.GetAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            _unitOfWork.TournamentRepository.Remove(result);

            return NoContent();
        }

        private async Task<bool> TournamentExists(int id)
        {
            return await _unitOfWork.TournamentRepository.AnyAsync(id);
        }
    }
}
