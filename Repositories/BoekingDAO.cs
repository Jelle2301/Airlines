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
    public class BoekingDAO : IBoekingDAO
    {
        private AirlineDbContext dbContext;
        public BoekingDAO(AirlineDbContext context)
        {
            this.dbContext = context;
        }
        public async Task AddAsync(Boeking entity)
        {
            try
            {
                await dbContext.Boekings.AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(BoekingDAO) in AddAsync");
                throw;
            }
        }

        public Task DeleteAsync(Boeking entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Boeking>?> GetAllAsync()
        {
            try
            {
                return await dbContext.Boekings.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(BoekingDAO) in GetAllAsync");
                throw;
            }
        }
    }
}
