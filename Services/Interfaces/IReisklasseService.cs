﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;

namespace Services.Interfaces
{
    public interface IReisklasseService : IService<Reisklasse>
    {
        Task<Reisklasse?> GetByIdAsync(int id);
    }
}
