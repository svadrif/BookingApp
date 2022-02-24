using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {   
        // for override
        new Task<List<AppUser>> GetAll();
        new Task<AppUser> GetById(Guid id);
        
        Task<IEnumerable<AppUser>> SearchAppUser(string searchedValue);
    }
}
