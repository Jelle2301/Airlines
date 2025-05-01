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
    public class SeizoenService : ISeizoenService
    {
        private ISeizoenDAO _seizoenDAO;
        public SeizoenService(ISeizoenDAO seizoenDAO)
        {
            _seizoenDAO = seizoenDAO;
        }
        public async Task<IEnumerable<Seizoen>?> GetAllAsync()
        {
            try
            {
                return await _seizoenDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(SeizoenService) in GetAllAsync");
                throw;
            }
        }

        public async Task<Seizoen?> GetByIdAsync(int id)
        {
            try
            {
                return await _seizoenDAO.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(SeizoenService) in GetAllAsync");
                throw;
            }
        }
    }
}
