﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Data.Models;

namespace OnlineShop.Services.Data.Interfaces
{
    public interface IGenderService
    {
        Task<IEnumerable<Gender>> GetAllGendersAsync();
    }
}
