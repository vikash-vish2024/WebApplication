﻿using System;
using System.Collections.Generic;

namespace Assesment_1.Models;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
