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
        new Task Add(AppUser entity);
        new Task<List<AppUser>> GetAll();
        new Task<AppUser> GetById(Guid id);
        new Task Update(AppUser entity);
        new Task Remove(AppUser entity);
        Task<IEnumerable<AppUser>> SearchAppUser(string searchedValue);
    }
}
