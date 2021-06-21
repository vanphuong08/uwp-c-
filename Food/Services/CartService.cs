﻿using Food3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food3.Adapters
{
   interface CartService
    {
        List<CartItem> GetCarts();
        bool AddToCart(CartItem item);
        bool DeleteItem(CartItem item);
        bool UpdateQty(CartItem item, int newQty);
        bool ClearCart();
      
    }
}
