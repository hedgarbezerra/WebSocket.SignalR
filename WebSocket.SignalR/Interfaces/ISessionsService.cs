using System.Linq.Expressions;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Interfaces
{
    public interface ISessionsService
    {
        Session? GetSession(Guid sessionId);
        IReadOnlyList<Session> GetSessions();
        IReadOnlyList<Session> GetVacantSessions();
        IReadOnlyList<Session> GetSessions(Expression<Func<Session, bool>> filter);
        PaginatedList<Session> GetSessions(PaginationInput pagination, string route);
        Guid AddSession(Session session);
        bool SessionExists(Guid sessionId);
        bool UpdateSession(Session session);
        bool DeleteSession(Guid sessionId);
        bool AssignSeatToUserSession(Seat seat, AppUser user, Session session);
        bool AssignSeatToUserSession(Guid seatId, Guid userId, Guid sessionId);
        bool AssignSeatToUserSession(Guid userId, Guid sessionId, IEnumerable<Guid> seatsIds);
    }
}
