using Domains.Data;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserDAO : IDAO<AspNetUser>
    {
        private AirlineDbContext dbContext;

        public UserDAO(AirlineDbContext context)
        {
            this.dbContext = context;
        }
        public async Task<IEnumerable<AspNetUser>?> GetAllAsync()
        {
            try
            {
                return await dbContext.AspNetUsers.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in DAO(UserDAO) in GetAllAsync");
                throw;
            }
        }
    }
}
