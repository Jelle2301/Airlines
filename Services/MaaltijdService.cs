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
                Console.WriteLine("Error in Service(MaaltijdService) in GetAllAsync");
                throw;
            }
        }

        

        public async Task<Maaltijd?> GetByIdAsync(int id)
        {
            try
            {
                return await _maaltijdDAO.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(MaaltijdService) in GetAllAsync");
                throw;
            }
        }
        public async Task<IEnumerable<Maaltijd>?> GetAllGewoneMaaltijdenAsync()
        {
            try
            {
                return await _maaltijdDAO.GetAllGewoneMaaltijdenAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(MaaltijdService) in GetAllAsync");
                throw;
            }
        }
        public async Task<Maaltijd?> GetSpecifiekeMaaltijdVoorPlaats(string bestemming)
        {
            try
            {
                return await _maaltijdDAO.GetSpecifiekeMaaltijdVoorPlaats(bestemming);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(MaaltijdService) in GetAllAsync");
                throw;
            }
        }
    }
}
