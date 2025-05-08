using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;

namespace Repositories.Interfaces
{
    public interface IBoekingDAO : IDAO<Boeking>
    {
        Task AddAsync(Boeking entity);
        Task DeleteAsync(Boeking entity);
        Task<IEnumerable<Boeking>?> GetAllBoekingenVanUser(string userId);
    }
}
