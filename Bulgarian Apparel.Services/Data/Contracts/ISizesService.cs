﻿using Bulgarian_Apparel.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Services.Contracts
{
    public interface ISizesService
    {
        IQueryable<Size> GetAll();

        Size GetSizeForGuid(Guid id);
        
        Size GetSizeForStringGuid(string id);
    }
}