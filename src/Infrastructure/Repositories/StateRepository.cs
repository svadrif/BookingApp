﻿using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        public StateRepository(ApplicationDbContext context) : base(context) { }

        public async Task<State> GetByUserIdAsync(Guid userId, bool tracking = false)
        {
            return await Search(x => x.UserId == userId,
                                tracking)
                        .FirstOrDefaultAsync();
        }
    }
}