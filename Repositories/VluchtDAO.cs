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
    public class VluchtDAO : IDAO<Vlucht>
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

        public Task<IEnumerable<Vlucht>?> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
