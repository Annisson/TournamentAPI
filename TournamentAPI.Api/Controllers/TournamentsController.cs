using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TournamentAPI.Core.Dto;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;

namespace TournamentAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TournamentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournaments()
        {
            var tournament = await _unitOfWork.TournamentRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TournamentDto>>(tournament));
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournament(int id)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TournamentDto>(tournament));
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, TournamentDto tournamentDto)
        {
            if (id != tournamentDto.Id)
            {
                return BadRequest();
            }

            var tournamentEntity = await _unitOfWork.TournamentRepository.GetAsync(id);

            if (tournamentEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(tournamentDto, tournamentEntity);

            _unitOfWork.TournamentRepository.Update(tournamentEntity);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDto>> PostTournament(TournamentDto tournamentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var tournament = _mapper.Map<Tournament>(tournamentDto);
                _unitOfWork.TournamentRepository.Add(tournament);

                tournamentDto.Id = tournament.Id;

                return CreatedAtAction("GetTournament", new { id = tournamentDto.Id }, tournamentDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

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
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        private async Task<bool> TournamentExists(int id)
        {
            return await _unitOfWork.TournamentRepository.AnyAsync(id);
        }
    }
}
