﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IDAO<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllAsync();
        Task<IEnumerable<T>?> GetByIdAsync(int Id);
    }
}
