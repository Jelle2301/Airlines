using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;
using Repositories;
using Repositories.Interfaces;

namespace Services
{
    public class BoekingService : IBoekingDAO
    {
        private IBoekingDAO _boekingDAO;
        public BoekingService(IBoekingDAO boekingDAO)
        {
            _boekingDAO = boekingDAO;
        }
        public Task AddAsync(Boeking entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Boeking entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Boeking>?> GetAllAsync()
        {
            try
            {
                return await _boekingDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(BoekingService) in GetAllAsync");
                throw;
            }
        }
    }
}
