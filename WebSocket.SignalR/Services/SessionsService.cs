using System.Linq.Expressions;
using WebSocket.SignalR.Configuration;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Services
{
    [BindInterface(typeof(ISessionsService))]
    public class SessionsService : ISessionsService
    {
        private readonly IRepository<Session> _sessionsRepository;
        private readonly IRepository<SeatTaken> _seatsRepository;
        private readonly IUriService _uriService;

        public SessionsService(IRepository<Session> sessionsRepository, IRepository<SeatTaken> seatsRepository, IUriService uriService)
        {
            ArgumentNullException.ThrowIfNull(sessionsRepository, nameof(sessionsRepository));
            ArgumentNullException.ThrowIfNull(seatsRepository, nameof(seatsRepository));
            ArgumentNullException.ThrowIfNull(uriService, nameof(uriService));

            _sessionsRepository = sessionsRepository;
            _seatsRepository = seatsRepository;
            _uriService = uriService;
        }

        public Guid AddSession(Session session)
        {
            ArgumentNullException.ThrowIfNull(session);

            var insertedRoom = _sessionsRepository.Add(session);
            _sessionsRepository.SaveChanges();

            return insertedRoom.Id;
        }

        public bool UpdateSession(Session session)
        {
            ArgumentNullException.ThrowIfNull(session);

            var updatedRoom = _sessionsRepository.Update(session);

            return _sessionsRepository.SaveChanges();
        }

        public bool DeleteSession(Guid sessionId)
        {
            if (sessionId == Guid.Empty)
                throw new ArgumentNullException(nameof(sessionId));

            _sessionsRepository.Delete(sessionId);
            return _sessionsRepository.SaveChanges();
        }

        public Session? GetSession(Guid sessionId)
        {
            var session = _sessionsRepository.Get(sessionId);

            return session;
        }

        public IReadOnlyList<Session> GetSessions()
        {
            var sessions = _sessionsRepository.Get().ToList().AsReadOnly();

            return sessions;
        }

        public IReadOnlyList<Session> GetSessions(Expression<Func<Session, bool>> filter)
        {
            var sessions = _sessionsRepository.Get(filter).ToList().AsReadOnly();

            return sessions;
        }

        public PaginatedList<Session> GetSessions(PaginationInput pagination, string route)
        {
            var sessions = _sessionsRepository.Get();
            if (!string.IsNullOrEmpty(pagination.SearchTerm))
            {
                var actualTerm = pagination.SearchTerm.Split(' ');
                for (int i = 0; i < actualTerm.Length; i++)
                {
                    var term = actualTerm[i];
                    sessions = sessions.Where(t => t.Movie.Name.Contains(term) || t.Language.Contains(term)|| t.Movie.Genres.Select(g => g.Name).Any(g => g.Contains(term)));
                }
            }

            var paginated = new PaginatedList<Session>(sessions, _uriService, route, pagination.Index, pagination.Size);
            return paginated;
        }

        public IReadOnlyList<Session> GetVacantSessions()
        {
            var sessions = _sessionsRepository.Get(s => !s.IsFull).ToList().AsReadOnly();

            return sessions;
        }

        public bool AssignSeatToUserSession(Seat seat, AppUser user, Session session)
        {
            ArgumentNullException.ThrowIfNull(seat, nameof(seat));
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            ArgumentNullException.ThrowIfNull(session, nameof(session));

            var seatTaken = SeatTaken.Create(user.Id, session.Id, seat.Id);

            _seatsRepository.Add(seatTaken);

            return _seatsRepository.SaveChanges();
        }

        public bool AssignSeatToUserSession(Guid seatId, Guid userId, Guid sessionId)
        {
            var session = GetSession(sessionId);
            if (session is null)
                return false;

            var seatTaken = SeatTaken.Create(userId, sessionId, seatId);

            _seatsRepository.Add(seatTaken);

            return _seatsRepository.SaveChanges();
        }
    }
}
