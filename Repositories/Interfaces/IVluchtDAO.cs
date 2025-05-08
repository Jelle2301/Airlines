using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;

namespace Repositories.Interfaces
{
    public interface IVluchtDAO : IDAO<Vlucht>
    {
        Task<IEnumerable<Vlucht>?> GetVluchtenTussenPlaatsen(int vertrekPlaatdId, int bestemmingId);
        Task<IEnumerable<Vlucht>?> GetNormaleVluchtenTussenPlaatsen(int vertrekPlaatdId, int bestemmingId);
        Task<IEnumerable<Vlucht>?> GetNormaleVluchtenTussenPlaatsenTussenDatums(int vertrekPlaatdId, int bestemmingId, DateTime startDatum, DateTime eindDatum);
        Task<IEnumerable<Vlucht>?> GetOverstappenVanVlucht(int vluchtId);
        Task<Vlucht?> GetByIdAsync(int id);
    }
}
