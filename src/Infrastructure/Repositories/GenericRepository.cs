using Application.Interfaces;
using Domain.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> DbSet;

        protected GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }
        public IQueryable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public virtual async Task AddAsync(T entity)
        {
            DbSet.Add(entity);
            await SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            DbSet.Update(entity);
            await SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(T entity)
        {
            DbSet.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
    }
}
