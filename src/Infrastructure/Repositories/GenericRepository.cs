using Application.Interfaces.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> DbSet;
        protected readonly ILogger _logger;

        protected GenericRepository(
            ApplicationDbContext context,
            ILogger logger)
        {
            _context = context;
            _logger = logger;
            DbSet = context.Set<T>();
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> predicate, bool tracking)
        {
            return !tracking ? DbSet.Where(predicate).AsNoTracking()
                             : DbSet.Where(predicate);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual IQueryable<T> GetAll(bool tracking)
        {
            return !tracking ? DbSet.AsNoTracking()
                             : DbSet;
        }

        public virtual async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}
