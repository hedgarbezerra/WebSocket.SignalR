using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebSocket.SignalR.Interfaces;

namespace WebSocket.SignalR.Data.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T Add(T obj) => _dbContext.Add(obj).Entity;
        public virtual void AddRange(IEnumerable<T> objs) => _dbContext.AddRange(objs);

        public virtual void Delete(Guid id)
        {
            T obj = Get(id);
            if (obj is null)
                throw new InvalidOperationException($"{typeof(T)} not found. Unable to delete unexisting object.");

            _dbContext.Entry(obj).State = EntityState.Deleted;
            _dbContext.Remove(obj);
        }

        public virtual T? Get(Guid id)
            => _dbContext.Set<T>().Find(id);

        public virtual IQueryable<T> Get() => _dbContext.Set<T>().AsQueryable();
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter)
            => Get().Where(filter);

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, object>>? order = null, int? count = 0, int? skip = 0, bool reverse = false)
        {
            var result = Get(filter);
            if (order is not null)
                result = reverse ? result.OrderByDescending(order) : result.OrderBy(order);
            if (count > 0 && skip > 0)
                result = result.Take((int)count)
                    .Skip((int)skip);

            return result;
        }
        public virtual T Update(T obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            return _dbContext.Update(obj).Entity;
        }
        public void DisposeContext() => _dbContext.Dispose();
        public DbContext GetDbContext() => _dbContext;
        public bool SaveChanges() => _dbContext.SaveChanges() > 0;
    }
}
