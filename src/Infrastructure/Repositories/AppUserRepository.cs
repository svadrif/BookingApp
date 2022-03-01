using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<List<AppUser>> GetAllAsync()
        {
            return await _context.AppUsers.AsNoTracking()
                .OrderBy(b => b.LastName)
                .ToListAsync();
        }

        public override async Task<AppUser> GetByIdAsync(Guid id)
        {
            return await _context.AppUsers.AsNoTracking()
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }        

        public async Task<IEnumerable<AppUser>> SearchAppUserAsync(string searchedValue)
        {
            return await _context.AppUsers.AsNoTracking()                
                .Where(b => b.UserName.Contains(searchedValue) ||
                            b.LastName.Contains(searchedValue) ||
                            b.FirstName.Contains(searchedValue) ||
                            b.Email.Contains(searchedValue))
                .ToListAsync();
        }
    }
}
