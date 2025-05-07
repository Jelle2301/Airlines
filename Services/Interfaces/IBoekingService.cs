using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;
using Repositories.Interfaces;

namespace Services.Interfaces
{
    public interface IBoekingService : IService<Boeking>
    {
        Task AddAsync(Boeking entity);
        Task DeleteAsync(Boeking entity);
    }
}
