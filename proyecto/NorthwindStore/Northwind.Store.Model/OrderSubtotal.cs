﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Northwind.Store.Model
{
    public partial class OrderSubtotal
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
