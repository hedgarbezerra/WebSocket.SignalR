using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using WebSocket.SignalR.Configuration;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Services
{
    [BindInterface(typeof(ISessionsService))]
    public class SessionsService : ISessionsService
    {
        private const string defaultDateFormat = "dd/MM/yyyy 'at' HH:mm";

        private readonly IRepository<Session> _sessionsRepository;
        private readonly IRepository<SeatTaken> _seatsRepository;
        private readonly IUriService _uriService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMoviesService _moviesService;
        private readonly IRoomsService _roomsService;

        public SessionsService(IRepository<Session> sessionsRepository, IRepository<SeatTaken> seatsRepository,
            IUriService uriService, UserManager<AppUser> userManager, IMoviesService moviesService, IRoomsService roomsService)
        {
            ArgumentNullException.ThrowIfNull(sessionsRepository, nameof(sessionsRepository));
            ArgumentNullException.ThrowIfNull(seatsRepository, nameof(seatsRepository));
            ArgumentNullException.ThrowIfNull(uriService, nameof(uriService));
            ArgumentNullException.ThrowIfNull(moviesService, nameof(moviesService));
            ArgumentNullException.ThrowIfNull(roomsService, nameof(roomsService));

            _sessionsRepository = sessionsRepository;
            _seatsRepository = seatsRepository;
            _uriService = uriService;
            _userManager = userManager;
            _moviesService = moviesService;
            _roomsService = roomsService;
        }

        public Result<Guid> AddSession(Session session)
        {
            if (session is null)
                return Result.Fail($"The parameter '{nameof(session)}' provided cannot be null.");

            var resultMovie = _moviesService.GetMovie(session.MovieId);
            var resultRoom = _roomsService.GetRoom(session.RoomId);
            var resultValidations = Result.Merge(resultMovie.ToResult(), resultRoom.ToResult());
            if(resultValidations.IsFailed)
                return resultValidations;

            bool existingSessionInRoom = _sessionsRepository.Get(s => s.RoomId == session.RoomId &&
                (s.Date >= session.Date && s.Date <= session.Date.AddMinutes(resultMovie.ValueOrDefault.Duration.TotalMinutes)))
                .Any();
            if (existingSessionInRoom)
                return Result.Fail($"The room '{resultRoom.Value.Name}' won't be available at {session.Date.ToString(defaultDateFormat)}");

            var insertedSession = _sessionsRepository.Add(session);            

            return Result.Ok(_sessionsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok(insertedSession.Id).WithSuccess($"The session at '{session.Date.ToString(defaultDateFormat)}' was created with identifier '{insertedSession.Id}'.")
                    : Result.Fail($"The session at '{session.Date.ToString(defaultDateFormat)}' was not created."));
        }
        public Result UpdateSession(Session session)
        {
            if (session is null)
                return Result.Fail($"The parameter '{nameof(session)}' provided cannot be null.");

            var resultMovieExists = _moviesService.MovieExists(session.MovieId);
            var resultRoomExists = _roomsService.RoomExists(session.RoomId);
            var resultValidations = Result.Merge(resultMovieExists, resultRoomExists);
            if (resultValidations.IsFailed)
                return resultValidations;

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

        public Result AssignSeatToUserSession(Seat seat, Session session, AppUser user)
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
        public Result AssignSeatToUserSession(Guid seatId, Guid sessionId, AppUser user)
        {
            var result = Result.Ok();
            if (seatId == Guid.Empty)
                result.WithError($"The parameter '{nameof(seatId)}' cannot be null or empty.");
            if (user is null)
                result.WithError($"The parameter '{nameof(user)}' cannot be null or empty.");
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

            if(!IsUserOfAge(user, sessionResult.Value))
                return Result.Fail("The users age is lower than recommended.");

            var seatTaken = SeatTaken.Create(user.Id, sessionId, seatId);

            _seatsRepository.Add(seatTaken);

            return Result.Ok(_sessionsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Seat was assigned to the user '{user.Name}' successfully.")
                    : Result.Fail($"Seat was not assigned to the user '{user.Name}'."));
        }

        public Result AssignSeatToUserSession(Guid sessionId, IEnumerable<Guid> seatsIds, AppUser user)
        {
            var result = Result.Ok();
            if (!seatsIds.Any())
                result.WithError($"The parameter '{nameof(seatsIds)}' cannot be null or empty.");
            if (user is null)
                result.WithError($"The parameter '{nameof(user)}' cannot be null or empty.");
            if (sessionId == Guid.Empty)
                result.WithError($"The parameter '{nameof(sessionId)}' cannot be null or empty.");

            if (result.IsFailed)
                return result;

            var sessionResult = GetSession(sessionId);
            if (sessionResult.IsFailed)
                return sessionResult.ToResult();

            if (!IsUserOfAge(user, sessionResult.Value))
                return Result.Fail("The users age is lower then recommended.");

            var takenSeats = seatsIds.Select(s => _seatsRepository.Get(st => st.SeatId == s && st.SessionId == sessionId));
            if (takenSeats.Any())
                return Result.Fail("Some of the seats may have been taking, unable to proceed.");

            var seatsTakenCreated = seatsIds.Select(seatId => SeatTaken.Create(user.Id, sessionId, seatId));

            _seatsRepository.AddRange(seatsTakenCreated);

            return Result.Ok(_seatsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Seat was assigned to the user '{user.Name}' successfully.")
                    : Result.Fail($"Seat was not assigned to the user '{user.Name}'."));
        }

        public Result<List<SeatTaken>> GetSeatsTaken(Guid sessionId)
        {
            var session = GetSession(sessionId);

            return Result.Ok(session.Value.SeatsTaken)
                .WithSuccess($"Total count of seats taken: {session.Value.SeatsTaken.Count}");
        }

        public Result<List<Seat>> GetSeatsEmpty(Guid sessionId)
        {
            var session = GetSession(sessionId);
            if (session.IsFailed)
                return session.ToResult();

            var room = session.ValueOrDefault.Room;
            var availableSeats = room.Seats.Where(s => !session.ValueOrDefault.SeatsTaken.Exists(st => st.SeatId == s.Id));
            
            return Result.Ok(availableSeats.ToList())
                .WithSuccess($"Total count of seats available: {availableSeats.Count()}");
        }

        private bool IsUserOfAge(AppUser user, Session session)
        {
            DateTime minClassificationBirthdate = DateTime.Now.AddYears(-session.Movie.Classification);

            return user.Birthdate <= minClassificationBirthdate;
        }
    }
}
