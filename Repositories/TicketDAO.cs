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
        public Task AddAsync(Ticket entity)
        {
            throw new NotImplementedException();
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
