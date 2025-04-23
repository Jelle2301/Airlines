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
    public class MaaltijdService : IMaaltijdService
    {
        private IMaaltijdDAO _maaltijdDAO;
        public MaaltijdService(IMaaltijdDAO maaltijdDAO)
        {
            _maaltijdDAO = maaltijdDAO;
        }
        public async Task<IEnumerable<Maaltijd>?> GetAllAsync()
        {
            try
            {
                return await _maaltijdDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(VluchtService) in GetAllAsync");
                throw;
            }
        }

        public async Task<Maaltijd?> GetByNaamAsync(string naam)
        {
            try
            {
                return await _maaltijdDAO.GetByNaamAsync(naam);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(PlaatsService) in GetAllAsync");
                throw;
            }
        }
    }
}
