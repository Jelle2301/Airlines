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
        private IBoekingService _boekingService;
        public BoekingService(IBoekingService boekingService)
        {
            _boekingService = boekingService;
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
                return await _boekingService.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(BoekingService) in GetAllAsync");
                throw;
            }
        }
    }
}
