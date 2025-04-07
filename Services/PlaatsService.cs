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
    public class PlaatsService : IService<Plaats>
    {
        private IDAO<Plaats> _plaatsDAO;
       public PlaatsService(IDAO<Plaats> plaatsDAO)
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
                Console.WriteLine("Error in Service(LuchthavenService) in GetAllAsync");
                throw;
            }
        }

        
    }
}
