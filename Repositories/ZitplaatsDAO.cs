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
    public class ZitplaatsDAO : IZitplaatsDAO
    {
        private AirlineDbContext dbContext;
        public ZitplaatsDAO(AirlineDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<IEnumerable<Zitplaat>?> GetAllAsync()
        {
            try
            {
                return await dbContext.Zitplaats.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(ZitplaatsDAO) in GetAllAsync");
                throw;
            }
        }

        public async Task<IEnumerable<Zitplaat>?> GetAllZitplaatsenByVluchtAsync(int vluchtId)
        {
            try
            {
                // return await dbContext.Zitplaats.Where(v => v.VluchtId = vluchtId).Where(v => v.isBezet == false).ToListAsync();
                return await dbContext.Zitplaats.ToListAsync();//is nu voor geen errors te krijgen maar zal het bovenstaande gebruiken na dat in database aangepast is
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(ZitplaatsDAO) in GetAllAsync");
                throw;
            }
        }
    }
}
