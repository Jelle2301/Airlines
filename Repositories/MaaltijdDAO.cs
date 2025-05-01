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
    public class MaaltijdDAO : IMaaltijdDAO
    {
        private AirlineDbContext dbContext;
        public MaaltijdDAO(AirlineDbContext context)
        {
            this.dbContext = context;
        }
        public async Task<IEnumerable<Maaltijd>?> GetAllAsync()
        {
            try
            {
                return await dbContext.Maaltijds.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(MaaltijdDAO) in GetAllAsync");
                throw;
            }
        }

        public async Task<Maaltijd?> GetByIdAsync(int id)
        {
            try
            {
                return await dbContext.Maaltijds.Where(v => v.MaaltijdId.Equals(id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(MaaltijdDAO) in GetAllAsync");
                throw;
            }
        }
        public async Task<IEnumerable<Maaltijd>?> GetAllGewoneMaaltijdenAsync()
        {
            try
            {
                return await dbContext.Maaltijds.Where(v => v.PlaatsMaaltijd.Equals("Overal")).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(MaaltijdDAO) in GetAllAsync");
                throw;
            }
        }
        public async Task<Maaltijd?> GetSpecifiekeMaaltijdVoorPlaats()
        {
            try
            {
                return await dbContext.Maaltijds.Where(v => !v.PlaatsMaaltijd.Equals("Overal")).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(MaaltijdDAO) in GetAllAsync");
                throw;
            }
        }
    }
}
