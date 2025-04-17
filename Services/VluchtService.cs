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
    public class VluchtService : IService<Vlucht>
    {
        private IDAO<Vlucht> _vluchtDAO;
        public VluchtService(IDAO<Vlucht> vluchtDAO)
        {
            _vluchtDAO = vluchtDAO;
        }
        public async Task<IEnumerable<Vlucht>?> GetAllAsync()
        {
            try
            {
                return await _vluchtDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(VluchtService) in GetAllAsync");
                throw;
            }
        }

        

        public Task<Plaats?> GetByNaamAsync(string naam)
        {
            throw new NotImplementedException();
        }
    }
}
