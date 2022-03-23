using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> DbSet;
        protected readonly ILoggerManager _logger;

        protected GenericRepository(
            ApplicationDbContext context,
            ILoggerManager logger
            )
        {
            _context = context;
            _logger = logger;
            DbSet = context.Set<T>();
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> predicate, bool tracking)
        {
            try
            {
                return !tracking ? DbSet.Where(predicate).AsNoTracking()
                             : DbSet.Where(predicate);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "{Repo} Search method has generated an error", typeof(GenericRepository<T>));
                return null;
            }
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                return await DbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "{Repo} GetByIdAsync method has generated an error", typeof(GenericRepository<T>));
                return null;
            }
        }

        public virtual IQueryable<T> GetAll(bool tracking)
        {
            try
            {
                return !tracking ? DbSet.AsNoTracking()
                                 : DbSet;
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "{Repo} GetAll method has generated an error", typeof(GenericRepository<T>));
                return null;
            }
        }

        public virtual async Task AddAsync(T entity)
        {
            try
            {
                await DbSet.AddAsync(entity);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "{Repo} AddAsync method has generated an error", typeof(GenericRepository<T>));
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                DbSet.Update(entity);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "{Repo} Update method has generated an error", typeof(GenericRepository<T>));
            }
        }

        public virtual void Remove(T entity)
        {
            try
            {
                DbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "{Repo} Remove method has generated an error", typeof(GenericRepository<T>));
            }
        }
    }
}
