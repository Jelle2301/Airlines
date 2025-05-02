using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;

namespace Repositories.Interfaces
{
    public interface IMaaltijdDAO : IDAO<Maaltijd>
    {
        Task<Maaltijd?> GetByIdAsync(int id);
        Task<IEnumerable<Maaltijd>?> GetAllGewoneMaaltijdenAsync();
        Task<Maaltijd?> GetSpecifiekeMaaltijdVoorPlaats(string bestemming);
    }
}
