﻿using Bulgarian_Apparel.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Services.Contracts
{
    public interface ISizesAvailableService
    {
        IQueryable<Size> GetAll();
    }
}
