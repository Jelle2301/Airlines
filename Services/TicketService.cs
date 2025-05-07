using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class TicketService : ITicketService
    {
        private ITicketDAO _ticketDAO;
        public TicketService(ITicketDAO ticketDAO)
        {
            _ticketDAO = ticketDAO;
        }
        public async Task AddAsync(Ticket entity)
        {
            try
            {
                await _ticketDAO.AddAsync(entity);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(TicketService) in AddAsync");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>?> GetAllAsync()
        {
            try
            {
                return await _ticketDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(TicketService) in GetAllAsync");
                throw;
            }
        }
    }
}
