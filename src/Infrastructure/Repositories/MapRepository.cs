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
    public class MapRepository : GenericRepository<Map>, IMapRepository
    {
        public MapRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
