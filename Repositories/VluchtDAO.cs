using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Data;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class VluchtDAO : IVluchtDAO
    {
        private AirlineDbContext dbContext;
        public VluchtDAO(AirlineDbContext context)
        {
            this.dbContext = context;
        }
        public async Task<IEnumerable<Vlucht>?> GetAllAsync()
        {
            try
            {
                return await dbContext.Vluchts.Include(v=> v.Vliegtuig).Include(v=> v.Vertrekplaats).Include(v => v.Bestemming).ThenInclude(b => b.Plaats).ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(VluchtDAO) in GetAllAsync");
                throw;
            }
        }

        public async Task<IEnumerable<Vlucht>?> GetVluchtenTussenPlaatsen(int vertrekPlaatdId, int bestemmingId)
        {
            try
            {
                return await dbContext.Vluchts.Include(v => v.Vliegtuig).Include(v => v.Vertrekplaats).Where(v => v.VertrekplaatsId ==vertrekPlaatdId).Include(v => v.Bestemming).ThenInclude(b => b.Plaats).Where(v => v.BestemmingId == bestemmingId).ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(VluchtDAO) in GetAllAsync");
                throw;
            }
        }
        public async Task<Vlucht?> GetByIdAsync(int id)
        {
            try
            {
                return await dbContext.Vluchts.Where(v => v.VluchtId.Equals(id)).Include(v=> v.Vliegtuig).Include(v => v.Vertrekplaats).ThenInclude(b => b.Plaats).Include(v => v.Bestemming).ThenInclude(b => b.Plaats).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(VluchtDAO) in GetByIdAsync");
                throw;
            }
        }

        public async Task<IEnumerable<Vlucht>?> GetNormaleVluchtenTussenPlaatsen(int vertrekPlaatdId, int bestemmingId)
        {
            try
            {
                return await dbContext.Vluchts.Where(v=> v.IsOverstap == false).Include(v => v.Vliegtuig).Include(v => v.Vertrekplaats).Where(v => v.VertrekplaatsId == vertrekPlaatdId).Include(v => v.Bestemming).ThenInclude(b => b.Plaats).Where(v => v.BestemmingId == bestemmingId).ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(VluchtDAO) in GetNormaleVluchtenTussenPlaatsen");
                throw;
            }
        }
        
        public async Task<IEnumerable<Vlucht>?> GetOverstappenVanVlucht(int vluchtId)
        {
            try
            {
                

                var overstappen = new List<Vlucht>();

                var huidigeVlucht = await dbContext.Vluchts
                                                   .Include(v => v.Vliegtuig)
                                                   .Include(v => v.Vertrekplaats)
                                                   .Include(v => v.Bestemming)
                                                   .ThenInclude(b => b.Plaats)
                                                   .FirstOrDefaultAsync(v => v.VluchtId == vluchtId);

                while (huidigeVlucht != null && huidigeVlucht.EventueleVolgendeOverstapId != null)
                {
                    huidigeVlucht = await dbContext.Vluchts
                                                   .Include(v => v.Vliegtuig)
                                                   .Include(v => v.Vertrekplaats)
                                                   .Include(v => v.Bestemming)
                                                   .ThenInclude(b => b.Plaats)
                                                   .FirstOrDefaultAsync(v => v.VluchtId == huidigeVlucht.EventueleVolgendeOverstapId);

                    if (huidigeVlucht != null)
                    {
                        overstappen.Add(huidigeVlucht);
                    }



                }
                return overstappen;
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(VluchtDAO) in GetOverstappenVanVlucht");
                throw;
            }
        }
        
        
    }
}
