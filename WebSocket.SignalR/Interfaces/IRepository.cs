using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebSocket.SignalR.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Add(T obj);
        T Update(T obj);
        void Delete(Guid id);
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> filter);
        IQueryable<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, object>>? order = null, int? count = 0, int? skip = 0, bool reverse = false);
        T? Get(Guid id);
        DbContext GetDbContext();
        bool SaveChanges();
    }
}
