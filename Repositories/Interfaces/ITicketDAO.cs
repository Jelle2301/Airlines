using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;

namespace Repositories.Interfaces
{
    public interface ITicketDAO : IDAO<Ticket>
    {
        Task AddAsync(Ticket entity);
    }
}
