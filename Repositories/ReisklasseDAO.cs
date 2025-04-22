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
    public class ReisklasseDAO : IReisklasseDAO
    {
        private AirlineDbContext dbContext;
        public ReisklasseDAO(AirlineDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<IEnumerable<Reisklasse>?> GetAllAsync()
        {
            try
            {
                return await dbContext.Reisklasses.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(ReisklasseDAO) in GetAllAsync");
                throw;
            }
        }
    }
}
