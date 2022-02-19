using Application.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            AppUsers = new AppUserRepository(_context);
            Maps = new MapRepository(_context);
            Offices = new OfficeRepository(_context);
        }
        public IAppUserRepository AppUsers { get; private set; }
        public IMapRepository Maps { get; private set; }
        public IOfficeRepository Offices { get; private set; }

        public async Task <int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
