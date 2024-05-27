using TournamentAPI.Core.Repositories;
using TournamentAPI.Data.Data;

namespace TournamentAPI.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TournamentAPIApiContext _context;
        private TournamentRepository _tournamentRepository;
        private GameRepository _gameRepository;

        public UnitOfWork(TournamentAPIApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ITournamentRepository TournamentRepository
        {
            get
            {
                if(_tournamentRepository == null)
                {
                    _tournamentRepository = new TournamentRepository(_context);
                }
                return _tournamentRepository;
            }
        }

        public IGameRepository GameRepository
        {
            get
            {

                if (_gameRepository == null)
                {
                    _gameRepository = new GameRepository(_context);
                }
                return _gameRepository;
            }
        }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
