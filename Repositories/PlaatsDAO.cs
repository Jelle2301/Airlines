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
    public class PlaatsDAO : IPlaatsDAO
    {
        private AirlineDbContext dbContext;
        public PlaatsDAO(AirlineDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<IEnumerable<Plaats>?> GetAllAsync()
        {
            try
            {
                return await dbContext.Plaats.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(PlaatsDAO) in GetAllAsync");
                throw;
            }
        }

        public async Task<Plaats?> GetByNaamAsync(string naam)
        {
            try
            {
                return await dbContext.Plaats.Where(v => v.Naam.Equals(naam)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(PlaatsDAO) in GetAllAsync");
                throw;
            }
        }

        
    }
}
