using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;

namespace Services.Interfaces
{
    public interface IMaaltijdService : IService<Maaltijd>
    {
        Task<Maaltijd?> GetByNaamAsync(string naam);
    }
}
