﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<int> AddItemsToBasket(Guid basketId, List<Guid> itemsId);
    }
}
