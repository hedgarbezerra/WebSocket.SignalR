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
                .ForMember(p => p.Genres, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(p => p.Genres, opt => opt.Ignore());

            CreateMap<Genre, CreateGenreDTO>()
                .ReverseMap()
                .ForMember(p => p.Movies, opt => opt.Ignore());

            CreateMap<Seat, CreateSeatDTO>()
                .ReverseMap()
                .ForMember(p => p.Room, opt => opt.Ignore());

            CreateMap<Room, CreateRoomDTO>()
                .ReverseMap()
                .ForMember(p => p.Seats, opt => opt.Ignore())
                .ForMember(p => p.SeatCount, opt => opt.Ignore())
                .ForMember(p => p.IsEmpty, opt => opt.Ignore());

            CreateMap<Session, CreateSessionDTO>()
                .ReverseMap()
                .ForMember(p => p.IsFull, opt => opt.Ignore())
                .ForMember(p => p.Room, opt => opt.Ignore())
                .ForMember(p => p.Movie, opt => opt.Ignore())
                .ForMember(p => p.SeatsTaken, opt => opt.Ignore());


            CreateMap<Movie, UpdateMovieDTO>()
                .ForMember(p => p.Genres, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(p => p.Genres, opt => opt.Ignore());

            CreateMap<Genre, UpdateGenreDTO>()
                .ReverseMap()
                .ForMember(p => p.Movies, opt => opt.Ignore());

            CreateMap<Seat, UpdateSeatDTO>()
                .ReverseMap()
                .ForMember(p => p.Room, opt => opt.Ignore());

            CreateMap<Room, UpdateRoomDTO>()
                .ReverseMap()
                .ForMember(p => p.Seats, opt => opt.Ignore())
                .ForMember(p => p.SeatCount, opt => opt.Ignore())
                .ForMember(p => p.IsEmpty, opt => opt.Ignore());

            CreateMap<Session, UpdateSessionDTO>()
                .ReverseMap()
                .ForMember(p => p.IsFull, opt => opt.Ignore())
                .ForMember(p => p.Room, opt => opt.Ignore())
                .ForMember(p => p.Movie, opt => opt.Ignore())
                .ForMember(p => p.SeatsTaken, opt => opt.Ignore());

        }
    }
}
