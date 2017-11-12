﻿using Bulgarian_Apparel.Data.Models;
using System;
using System.Linq;

namespace Bulgarian_Apparel.Services
{
    public interface IShoppingCartService
    {
        IQueryable<ShoppingCart> GetAll();

        int Add(ShoppingCart cart);

        int Delete(ShoppingCart cart);

        int Update(ShoppingCart cart);

        double CalculateTotalPriceForCart(Guid guid);
    }
}