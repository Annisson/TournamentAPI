using AutoMapper;
using TournamentAPI.Core.Dto;
using TournamentAPI.Core.Entities;

namespace TournamentAPI.Data.Data
{
    public class TournamentMappings : Profile
    {
        public TournamentMappings()
        {
            CreateMap<Tournament, TournamentDto>().ReverseMap();
            CreateMap<Game, GameDto>().ReverseMap();
        }
    }
}
