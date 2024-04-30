using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebSocket.SignalR.Interfaces
{
    public interface IRepository<T, TId> where T : class
    {
        T Add(T obj);
        T Update(T obj);
        void Delete(TId id);
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> filter);
        IQueryable<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, object>>? order = null, int? count = 0, int? skip = 0, bool reverse = false);
        T Get(TId id);
        DbContext GetDbContext();
        void SaveChanges();
    }
}
