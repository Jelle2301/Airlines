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

        public async Task<Zitplaat?> GetAllBeschikbareZitplaatsenByVluchtAsync(int vluchtId)
        {
            try
            {
                 return await dbContext.Zitplaats.Where(v => v.VluchtId == vluchtId).Where(v => v.IsBezet == false).FirstOrDefaultAsync();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(ZitplaatsDAO) in GetAllBeschikbareZitplaatsenByVluchtAsync");
                throw;
            }
        }

        public async Task MaakZitplaatsBezet(int zitplaatdId)
        {
           await dbContext.Zitplaats.Where(v => v.ZitplaatsId == zitplaatdId).ExecuteUpdateAsync(v => v.SetProperty(z => z.IsBezet, true));
        }

        public async Task MaakZitplaatsVrij(int zitplaatdId)
        {
            await dbContext.Zitplaats.Where(v => v.ZitplaatsId == zitplaatdId).ExecuteUpdateAsync(v => v.SetProperty(z => z.IsBezet, false));
        }
    }
}
