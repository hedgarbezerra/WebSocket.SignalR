using AutoMapper;
using WebSocket.SignalR.Models;
using WebSocket.SignalR.Models.DTOs;

namespace WebSocket.SignalR.Configuration
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            CreateMap<Movie, CreateMovieDTO>()
                .IncludeAllDerived()
                .ReverseMap()
                .ForMember(p => p.Genres, opt => opt.Ignore());

            CreateMap<Genre, CreateGenreDTO>()
                .IncludeAllDerived()
                .ReverseMap()
                .ForMember(p => p.Movies, opt => opt.Ignore());

            CreateMap<Seat, CreateSeatDTO>()
                .IncludeAllDerived()
                .ReverseMap()
                .ForMember(p => p.Room, opt => opt.Ignore());

            CreateMap<Room, CreateRoomDTO>()
                .IncludeAllDerived()
                .ReverseMap()
                .ForMember(p => p.Seats, opt => opt.Ignore())
                .ForMember(p => p.SeatCount, opt => opt.Ignore())
                .ForMember(p => p.IsEmpty, opt => opt.Ignore());

            CreateMap<Session, CreateSessionDTO>()
                .IncludeAllDerived()
                .ReverseMap()
                .ForMember(p => p.IsFull, opt => opt.Ignore())
                .ForMember(p => p.Room, opt => opt.Ignore())
                .ForMember(p => p.Movie, opt => opt.Ignore())
                .ForMember(p => p.SeatsTaken, opt => opt.Ignore());

            //CreateMap<Movie, UpdateMovieDTO>()
            //    .ReverseMap()
            //    .ForMember(p => p.Genres, opt => opt.Ignore());
        }
    }
}
