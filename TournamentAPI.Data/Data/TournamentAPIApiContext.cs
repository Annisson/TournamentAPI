﻿using Microsoft.EntityFrameworkCore;
using TournamentAPI.Core.Entities;

namespace TournamentAPI.Data.Data
{
    public class TournamentAPIApiContext : DbContext
    {
        public TournamentAPIApiContext (DbContextOptions<TournamentAPIApiContext> options)
            : base(options)
        {
        }

        public DbSet<Tournament> Tournament { get; set; } = default!;
        public DbSet<Game> Game { get; set; } = default!;
    }
}
