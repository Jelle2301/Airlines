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
    public class SeizoenDAO : ISeizoenDAO
    {
        private AirlineDbContext dbContext;
        public SeizoenDAO(AirlineDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<IEnumerable<Seizoen>?> GetAllAsync()
        {
            try
            {
                return await dbContext.Seizoens.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(SeizoenDAO) in GetAllAsync");
                throw;
            }
        }
    }
}
