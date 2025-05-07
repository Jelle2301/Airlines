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
    public class TicketDAO : ITicketDAO
    {
        private AirlineDbContext dbContext;
        public TicketDAO(AirlineDbContext context)
        {
            this.dbContext = context;
        }
        public async Task AddAsync(Ticket entity)
        {
            try
            {
                dbContext.Entry(entity.Maaltijd).State = EntityState.Unchanged;
                dbContext.Entry(entity.Reisklasse).State = EntityState.Unchanged;
                dbContext.Entry(entity.Vlucht).State = EntityState.Unchanged;
                await dbContext.Tickets.AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(TicketDAO) in AddAsync");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>?> GetAllAsync()
        {
            try
            {
                return await dbContext.Tickets.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO(TicketDAO) in GetAllAsync");
                throw;
            }
        }
    }
}
