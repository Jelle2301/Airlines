using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;
using Repositories.Interfaces;

namespace Services.Interfaces
{
    public interface ITicketService :IService<Ticket>
    {
        Task AddAsync(Ticket entity);
    }
    
}
