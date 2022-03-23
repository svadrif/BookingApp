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
                _logger.LogError($"Something went wrong in the {nameof(Search)} action {ex}");
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
                _logger.LogError($"Something went wrong in the {nameof(GetByIdAsync)} action {ex}");
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
                _logger.LogError($"Something went wrong in the {nameof(GetAll)} action {ex}");
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
                _logger.LogError($"Something went wrong in the {nameof(AddAsync)} action {ex}");
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
                _logger.LogError($"Something went wrong in the {nameof(Update)} action {ex}");
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
                _logger.LogError($"Something went wrong in the {nameof(Remove)} action {ex}");
            }
        }
    }
}
