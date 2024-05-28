using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TournamentAPI.Core.Dto;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;

namespace TournamentAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GamesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGame()
        {
            var game = await _unitOfWork.GameRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<GameDto>>(game));
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            var game = await _unitOfWork.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameDto>(game));
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameDto gameDto)
        {
            if (id != gameDto.Id)
            {
                return BadRequest();
            }

            var gameEntity = await _unitOfWork.GameRepository.GetAsync(id);

            if (gameEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(gameDto, gameEntity);

            _unitOfWork.GameRepository.Update(gameEntity);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame(GameDto gameDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var game = _mapper.Map<Game>(gameDto);
                _unitOfWork.GameRepository.Add(game);

                gameDto.Id = game.Id;

                return CreatedAtAction("GetGame", new { id = gameDto.Id }, gameDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.InnerException?.Message ?? ex.Message);
            }

        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var result = await _unitOfWork.GameRepository.GetAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            _unitOfWork.GameRepository.Remove(result);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        private async Task<bool> GameExists(int id)
        {
            return await _unitOfWork.GameRepository.AnyAsync(id);
        }
    }
}
