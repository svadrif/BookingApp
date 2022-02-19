using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAppUserRepository AppUsers { get; }
        IMapRepository Maps { get; }
        IOfficeRepository Offices { get; }
        Task <int> Complete();
    }
}
