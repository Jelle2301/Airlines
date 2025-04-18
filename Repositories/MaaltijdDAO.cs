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

        public async Task<Maaltijd?> GetByNaamAsync(string naam)
        {
            try
            {
                return await dbContext.Maaltijds.Where(v => v.Naam.Equals(naam)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(PlaatsDAO) in GetAllAsync");
                throw;
            }
        }
    }
}
