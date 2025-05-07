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
    public class BoekingService : IBoekingService
    {
        private IBoekingDAO _boekingDAO;
        public BoekingService(IBoekingDAO boekingDAO)
        {
            _boekingDAO = boekingDAO;
        }
        public async Task AddAsync(Boeking entity)
        {
            try
            {
                await _boekingDAO.AddAsync(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(BoekingService) in AddAsync");
                throw;
            }
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
