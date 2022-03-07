using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OfficeRepository : GenericRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(ApplicationDbContext context) : base(context) { }

    }
}
