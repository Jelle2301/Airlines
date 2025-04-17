using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;

namespace Repositories.Interfaces
{
    public interface IPlaatsDAO : IDAO<Plaats>
    {
        Task<Plaats?> GetByNaamAsync(string naam);
    }
}
