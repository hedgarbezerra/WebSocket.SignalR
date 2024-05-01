using WebSocket.SignalR.Configuration;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Data.Repository
{
    public static class Repositories
    {
        [BindInterface(typeof(IRepository<Room>))]
        public class RoomsRepository : BaseRepository<Room>
        {
            public RoomsRepository(AppDbContext dbContext) : base(dbContext)
            {
            }
        }

        [BindInterface(typeof(IRepository<Seat>))]
        public class SeatsRepository : BaseRepository<Seat>
        {
            public SeatsRepository(AppDbContext dbContext) : base(dbContext)
            {
            }
        }
        [BindInterface(typeof(IRepository<SeatTaken>))]
        public class SeatsTakenRepository : BaseRepository<Seat>
        {
            public SeatsTakenRepository(AppDbContext dbContext) : base(dbContext)
            {
            }
        }

        [BindInterface(typeof(IRepository<Movie>))]
        public class MoviesRepository : BaseRepository<Movie>
        {
            public MoviesRepository(AppDbContext dbContext) : base(dbContext)
            {
            }
        }

        [BindInterface(typeof(IRepository<Genre>))]
        public class GenresRepository : BaseRepository<Genre>
        {
            public GenresRepository(AppDbContext dbContext) : base(dbContext)
            {
            }
        }

        [BindInterface(typeof(IRepository<Session>))]
        public class SessionsRepository : BaseRepository<Session>
        {
            public SessionsRepository(AppDbContext dbContext) : base(dbContext)
            {
            }
        }
    }
}
