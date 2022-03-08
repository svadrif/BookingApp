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
        public async Task<AppUser> GetByTelegramId(long telegramId, bool tracking = false)
        {
           return await base.Search(x => x.TelegramId == telegramId, tracking).FirstOrDefaultAsync();
        }
    }
}
