using Microsoft.EntityFrameworkCore;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Data.Data;

namespace TournamentAPI.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly TournamentAPIApiContext _context;

        public GameRepository(TournamentAPIApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Game.ToListAsync();
        }

        public async Task<Game> GetAsync(int id)
        {
            return await _context.Game.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Game.AnyAsync(t => t.Id == id);
        }

        public void Add(Game game)
        {
            _context.Game.Add(game);
            _context.SaveChanges();
        }

        public void Update(Game game)
        {
            var existingEntity = _context.Game.Local.FirstOrDefault(entry => entry.Id == game.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }
            _context.Game.Update(game);
            _context.SaveChanges();
        }

        public void Remove(Game game)
        {
            _context.Game.Remove(game);
            _context.SaveChanges();
        }

    }
}
