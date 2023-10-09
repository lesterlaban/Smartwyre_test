using System;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var provider = InitializeServices();
        var service = provider.GetService<IRebateService>();

        CalculateRebateRequest request = new()
        {
            RebateIdentifier = args[0],
            ProductIdentifier = args[1],
            Volume = Convert.ToDecimal(args[2]),
        };

        var result = service.Calculate(request);
        Console.WriteLine($"Result: {result.Success}");
    }

   

    static ServiceProvider InitializeServices()
    {
         return new ServiceCollection()
            .AddSingleton<IRebateDataStore, RebateDataStore>()
            .AddSingleton<IProductDataStore, ProductDataStore>()
            .AddSingleton<IRebateService, RebateService>()
            .BuildServiceProvider();
    }

}
