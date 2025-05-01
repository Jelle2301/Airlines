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
    public class ReisklasseService : IReisklasseService
    {
        private IReisklasseDAO _reisklasseDAO;
        public ReisklasseService(IReisklasseDAO reisklasseDAO)
        {
            _reisklasseDAO = reisklasseDAO;
        }
        public async Task<IEnumerable<Reisklasse>?> GetAllAsync()
        {
            try
            {
                return await _reisklasseDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(ReisklasseService) in GetAllAsync");
                throw;
            }
        }

        public async Task<Reisklasse?> GetByIdAsync(int id)
        {
            try
            {
                return await _reisklasseDAO.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(MaaltijdService) in GetAllAsync");
                throw;
            }
        }
    }
}
