using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using WebSocket.SignalR.Configuration;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models;
using static WebSocket.SignalR.Data.Repository.Repositories;

namespace WebSocket.SignalR.Services
{
    [BindInterface(typeof(ISessionsService))]
    public class SessionsService : ISessionsService
    {
        private readonly IRepository<Session> _sessionsRepository;
        private readonly IRepository<SeatTaken> _seatsRepository;
        private readonly IUriService _uriService;
        private readonly UserManager<AppUser> _userManager;


        public SessionsService(IRepository<Session> sessionsRepository, IRepository<SeatTaken> seatsRepository, IUriService uriService, UserManager<AppUser> userManager)
        {
            ArgumentNullException.ThrowIfNull(sessionsRepository, nameof(sessionsRepository));
            ArgumentNullException.ThrowIfNull(seatsRepository, nameof(seatsRepository));
            ArgumentNullException.ThrowIfNull(uriService, nameof(uriService));

            _sessionsRepository = sessionsRepository;
            _seatsRepository = seatsRepository;
            _uriService = uriService;
            _userManager = userManager;
        }

        public Result<Guid> AddSession(Session session)
        {
            if (session is null)
                return Result.Fail($"The parameter '{nameof(session)}' provided cannot be null.");

            var insertedSession = _sessionsRepository.Add(session);
            

            return Result.Ok(_sessionsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"The session at '{session.Date.ToString("U")}' was created with identifier '{insertedSession.Id}'.")
                    : Result.Fail($"The session at '{session.Date.ToString("U")}' was not created."));
        }
        public Result UpdateSession(Session session)
        {
            if (session is null)
                return Result.Fail($"The parameter '{nameof(session)}' provided cannot be null.");

            var updatedSession = _sessionsRepository.Update(session);

            return Result.Ok(_sessionsRepository.SaveChanges())
                .Bind(v => v ? 
                    Result.Ok().WithSuccess($"Session with identifier '{session.Id}' updated successfully.") 
                    : Result.Fail($"Session with identifier '{session.Id}' was not updated."));
        }
        public Result DeleteSession(Guid sessionId)
        {
            var result = SessionExists(sessionId);
            if (result.IsFailed)
                return result;

            _sessionsRepository.Delete(sessionId);

            return Result.Ok(_sessionsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Session with identifier '{sessionId}' successfully deleted.")
                    : Result.Fail($"Session with identifier '{sessionId}' was not deleted."));
        }
        public Result SessionExists(Guid sessionId)
        {
            bool sessionFound = _sessionsRepository.Get(m => m.Id == sessionId).Any();

            return Result.OkIf(sessionFound, $"Session with identifier '{sessionId}' not found.");
        }
        public Result<Session?> GetSession(Guid sessionId)
        {
            var session = _sessionsRepository.Get(sessionId);

            return Result.Ok(session is not null)
                .Bind(v => v ? 
                    Result.Ok(session) 
                    :Result.Fail($"Session with identifier '{sessionId}' not found."));
        }
        public Result<List<Session>> GetSessions()
        {
            var sessions = _sessionsRepository.Get().ToList();

            return Result.Ok(sessions).WithSuccess($"Total count of sessions: {sessions.Count}.");
        }
        public Result<List<Session>> GetSessions(Expression<Func<Session, bool>> filter)
        {
            var sessions = _sessionsRepository.Get(filter).ToList();

            return Result.Ok(sessions).WithSuccess($"Total count of sessions: {sessions.Count}.");
        }
        public Result<PaginatedList<Session>> GetSessions(PaginationInput pagination, string route)
        {
            var sessions = _sessionsRepository.Get();
            if (!string.IsNullOrEmpty(pagination.SearchTerm))
            {
                var actualTerm = pagination.SearchTerm.Split(' ');
                for (int i = 0; i < actualTerm.Length; i++)
                {
                    var term = actualTerm[i];
                    sessions = sessions.Where(t => t.Movie.Name.Contains(term) || t.Language.Contains(term) || t.Movie.Genres.Select(g => g.Name).Any(g => g.Contains(term)));
                }
            }

            var paginated = new PaginatedList<Session>(sessions, _uriService, route, pagination.Index, pagination.Size);
            return Result.Ok(paginated).WithSuccess($"page '{paginated.PageIndex}' from '{paginated.TotalPages}' - Total itens: {paginated.TotalCount}");
        }
        public Result<List<Session>> GetVacantSessions()
        {
            var sessions = _sessionsRepository.Get(s => !s.IsFull).ToList();

            return Result.Ok(sessions);
        }

        public Result AssignSeatToUserSession(Seat seat, AppUser user, Session session)
        {
            var result = Result.Ok();
            if (seat is null)
                result.WithError($"The parameter '{nameof(seat)}' cannot be null.");
            if (user is null)
                result.WithError($"The parameter '{nameof(user)}' cannot be null.");
            if (session is null)
                result.WithError($"The parameter '{nameof(session)}' cannot be null.");

            if(result.IsFailed)
                return result;

            var isSeatTaken = _seatsRepository.Get(s => s.SeatId == seat!.Id && s.SessionId == session.Id).Any();
            if (isSeatTaken)
                return Result.Fail($"The selected seat is already taken.");

            if(!IsUserOfAge(user, session))
                return Result.Fail("The users age is lower then recommended.");

            var seatTaken = SeatTaken.Create(user.Id, session.Id, seat.Id);

            _seatsRepository.Add(seatTaken);

            return Result.Ok(_sessionsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Seat was assigned to the user '{user.Name}' successfully.")
                    : Result.Fail($"Seat was not assigned to the user '{user.Name}'."));
        }
        public Result AssignSeatToUserSession(Guid seatId, Guid userId, Guid sessionId)
        {
            var result = Result.Ok();
            if (seatId == Guid.Empty)
                result.WithError($"The parameter '{nameof(seatId)}' cannot be null or empty.");
            if (userId == Guid.Empty)
                result.WithError($"The parameter '{nameof(userId)}' cannot be null or empty.");
            if (sessionId == Guid.Empty)
                result.WithError($"The parameter '{nameof(sessionId)}' cannot be null or empty.");

            if (result.IsFailed)
                return result;

            var isSeatTaken = _seatsRepository.Get(s => s.SeatId == seatId && s.SessionId == sessionId).Any();
            if (isSeatTaken)
                return Result.Fail($"The selected seat is already taken.");

            var sessionResult = GetSession(sessionId);
            if (sessionResult.IsFailed)
                return sessionResult.ToResult();

            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if(!IsUserOfAge(user, sessionResult.Value))
                return Result.Fail("The users age is lower then recommended.");

            var seatTaken = SeatTaken.Create(userId, sessionId, seatId);

            _seatsRepository.Add(seatTaken);

            return Result.Ok(_sessionsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Seat was assigned to the user '{user.Name}' successfully.")
                    : Result.Fail($"Seat was not assigned to the user '{user.Name}'."));
        }
        public Result AssignSeatToUserSession(Guid userId, Guid sessionId, IEnumerable<Guid> seatsIds)
        {
            var result = Result.Ok();
            if (!seatsIds.Any())
                result.WithError($"The parameter '{nameof(seatsIds)}' cannot be null or empty.");
            if (userId == Guid.Empty)
                result.WithError($"The parameter '{nameof(userId)}' cannot be null or empty.");
            if (sessionId == Guid.Empty)
                result.WithError($"The parameter '{nameof(sessionId)}' cannot be null or empty.");

            if (result.IsFailed)
                return result;

            var sessionResult = GetSession(sessionId);
            if (sessionResult.IsFailed)
                return sessionResult.ToResult();

            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (!IsUserOfAge(user, sessionResult.Value))
                return Result.Fail("The users age is lower then recommended.");

            var takenSeats = seatsIds.Select(s => _seatsRepository.Get(st => st.SeatId == s && st.SessionId == sessionId));
            if (takenSeats.Any())
                return Result.Fail("Some of the seats may have been taking, unable to proceed.");

            var seatsTakenCreated = seatsIds.Select(seatId => SeatTaken.Create(userId, sessionId, seatId));

            _seatsRepository.AddRange(seatsTakenCreated);

            return Result.Ok(_seatsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Seat was assigned to the user '{user.Name}' successfully.")
                    : Result.Fail($"Seat was not assigned to the user '{user.Name}'."));
        }

        private bool IsUserOfAge(AppUser user, Session session)
        {
            DateTime minClassificationBirthdate = DateTime.Now.AddYears(-session.Movie.Classification);

            return user.Birthdate > minClassificationBirthdate;
        }

    }
}
