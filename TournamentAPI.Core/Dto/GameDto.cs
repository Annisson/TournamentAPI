﻿namespace TournamentAPI.Core.Dto
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int TournamentId { get; set; }

    }
}
