using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;

namespace Services.Interfaces
{
    public interface ISeizoenService : IService<Seizoen>
    {
        Task<Seizoen?> GetByIdAsync(int id);
    }
}
