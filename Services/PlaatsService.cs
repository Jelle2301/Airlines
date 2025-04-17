using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class PlaatsService : IPlaatsService
    {
       private IPlaatsDAO _plaatsDAO;
       public PlaatsService(IPlaatsDAO plaatsDAO)
        {
            _plaatsDAO = plaatsDAO;
        }

        public async Task<IEnumerable<Plaats>?> GetAllAsync()
        {
            try
            {
                return await _plaatsDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(PlaatsService) in GetAllAsync");
                throw;
            }
        }
        public async Task<Plaats?> GetByNaamAsync(string naam)
        {
            try
            {
                return await _plaatsDAO.GetByNaamAsync(naam);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(PlaatsService) in GetAllAsync");
                throw;
            }
        }


    }
}
