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
    public class ZitplaatsService :IZitplaatsService
    {
        private IZitplaatsDAO _zitplaatsDAO;
        public ZitplaatsService(IZitplaatsDAO zitplaatsDAO)
        {
            _zitplaatsDAO = zitplaatsDAO;
        }
        public async Task<IEnumerable<Zitplaat>?> GetAllAsync()
        {
            try
            {
                return await _zitplaatsDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(ZitplaatsService) in GetAllAsync");
                throw;
            }
        }

        
    }
}
