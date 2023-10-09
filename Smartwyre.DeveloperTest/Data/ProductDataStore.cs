﻿using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductDataStore
{
    public Product GetProduct(string productIdentifier)
    {
        return new Product() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate | SupportedIncentiveType.AmountPerUom | SupportedIncentiveType.FixedCashAmount };
    }
}
