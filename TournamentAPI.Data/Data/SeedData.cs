using Bogus;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TournamentAPI.Core.Entities;

namespace TournamentAPI.Data.Data
{
    public class SeedData
    {
        private static Faker faker;


        public static async Task InitAsync(TournamentAPIApiContext context)  
        {
            if (await context.Tournament.AnyAsync()) return; // Check if Tournament is empty, if it is it will continue otherwise it will return

            faker = new Faker("en");

            var tournaments = GenerateTournaments(2);
            await context.AddRangeAsync(tournaments);

            var games = GenerateGames(6, tournaments);
            await context.AddRangeAsync(games);

            await context.SaveChangesAsync();
        }

        private static IEnumerable<Tournament> GenerateTournaments(int numberOfTournaments)
        {
            var tournaments = new List<Tournament>();

            for (int i = 0; i < numberOfTournaments; i++)
            {
                var tournament = new Tournament { 
                    Title = "Tournament " + faker.Random.Word(),
                    StartDate = DateTime.Now
                };

                tournaments.Add(tournament);
            }

            return tournaments;
        }

        private static IEnumerable<Game> GenerateGames(int numberOfGames, IEnumerable<Tournament> tournaments)
        {
            var games = new List<Game>();

            foreach (var tournament in tournaments)
            {
                for (int i = 0; i < numberOfGames; i++)
                {
                    var game = new Game
                    {
                        Title = "Game " + faker.Random.Word(),
                        Time = faker.Date.Soon(),
                        Tournament = tournament
                    };

                    games.Add(game);
                }
            }

            return games;
        }

    }
}
