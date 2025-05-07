using Domains.Entities;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IService<AspNetUser>
    {
        private readonly IDAO<AspNetUser> userDAO;
        public UserService(IDAO<AspNetUser> userDAO)
        {
            this.userDAO = userDAO;
        }
        public async Task<IEnumerable<AspNetUser>?> GetAllAsync()
        {
            try
            {
                return await userDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(UserService) in GetAllAsync");
                throw;
            }
        }
    }
}
