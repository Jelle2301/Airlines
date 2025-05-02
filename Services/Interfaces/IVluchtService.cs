using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;

namespace Services.Interfaces
{
    public interface IVluchtService : IService<Plaats>
    {
        Task<IEnumerable<Vlucht>?> GetVluchtenTussenPlaatsen(int vertrekPlaatdId, int bestemmingId);
        Task<Vlucht?> GetByIdAsync(int id);
        Task<IEnumerable<Vlucht>?> GetNormaleVluchtenTussenPlaatsen(int vertrekPlaatdId, int bestemmingId);
        Task<IEnumerable<Vlucht>?> GetOverstappenVanVlucht(int vluchtId);

    }
}
