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
    public class VluchtService : IVluchtService
    {
        private IVluchtDAO _vluchtDAO;
        public VluchtService(IVluchtDAO vluchtDAO)
        {
            _vluchtDAO = vluchtDAO;
        }

        public Task<IEnumerable<Plaats>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<Vlucht>?> GetVluchtenTussenPlaatsen(int vertrekPlaatdId, int bestemmingId)
        {
            try
            {
                return await _vluchtDAO.GetVluchtenTussenPlaatsen(vertrekPlaatdId, bestemmingId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(VluchtService) in GetAllAsync");
                throw;
            }
        }

        public async Task<Vlucht?> GetByIdAsync(int id)
        {
            try
            {
                return await _vluchtDAO.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(VluchtService) in GetAllAsync");
                throw;
            }
        }

        public async Task<IEnumerable<Vlucht>?> GetNormaleVluchtenTussenPlaatsen(int vertrekPlaatdId, int bestemmingId)
        {
            try
            {
                return await _vluchtDAO.GetNormaleVluchtenTussenPlaatsen(vertrekPlaatdId, bestemmingId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(VluchtService) in GetAllAsync");
                throw;
            }
        }

        public async Task<IEnumerable<Vlucht>?> GetOverstappenVanVlucht(int vluchtId)
        {
            try
            {
                return await _vluchtDAO.GetOverstappenVanVlucht(vluchtId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(VluchtService) in GetAllAsync");
                throw;
            }
        }
    }
}
