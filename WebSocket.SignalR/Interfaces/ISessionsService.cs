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
        Result AssignSeatToUserSession(Seat seat, AppUser user, Session session);
        Result AssignSeatToUserSession(Guid seatId, Guid userId, Guid sessionId);
        Result AssignSeatToUserSession(Guid userId, Guid sessionId, IEnumerable<Guid> seatsIds);
    }
}
