using FluentResults;
using System.Linq.Expressions;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Interfaces
{
    public interface ISessionsService
    {
        Result<Session?> GetSession(Guid sessionId);
        Result<List<Session>> GetSessions();
        Result<List<Session>> GetVacantSessions();
        Result<List<Session>> GetSessions(Expression<Func<Session, bool>> filter);
        Result<PaginatedList<Session>> GetSessions(PaginationInput pagination, string route);
        Result<Guid> AddSession(Session session);
        Result SessionExists(Guid sessionId);
        Result UpdateSession(Session session);
        Result DeleteSession(Guid sessionId);
        Result AssignSeatToUserSession(Seat seat, Session session, AppUser user);
        Result AssignSeatToUserSession(Guid seatId, Guid sessionId, AppUser user);
        Result AssignSeatToUserSession(Guid sessionId, IEnumerable<Guid> seatsIds, AppUser user);
        Result<List<SeatTaken>> GetSeatsTaken(Guid sessionId);
        Result<List<Seat>> GetSeatsEmpty(Guid sessionId);
    }
}
