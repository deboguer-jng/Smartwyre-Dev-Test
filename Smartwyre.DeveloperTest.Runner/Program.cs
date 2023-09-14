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
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IProductDataStore, ProductDataStore>()
            .AddSingleton<IRebateDataStore, RebateDataStore>()
            .AddSingleton<IRebateIncentiveService, RebateIncentiveService>()
            .AddSingleton<IRebateService, RebateService>()
            .BuildServiceProvider();

        var rebateService = serviceProvider.GetRequiredService<IRebateService>();
        var rebateStore = serviceProvider.GetRequiredService<IRebateDataStore>();
        var productStore = serviceProvider.GetRequiredService<IProductDataStore>();

        var product = new Product()
        { 
            Identifier = "product1",
            SupportedIncentive = IncentiveType.FixedRateRebate,
            Uom = "uom",
            Price = 123,
            Id = 0
        };

        var rebate = new Rebate()
        {
            Identifier = "rebate1",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 100,
            Percentage = 10
        };

        productStore.SetProduct(product.Identifier, product);
        rebateStore.SetRebate(rebate.Identifier, rebate);

        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "product1",
            RebateIdentifier = "rebate1",
            Volume = 123
        };

        CalculateRebateResult result = rebateService.Calculate(request);

        var rebateStored = rebateStore.GetRebate(rebate.Identifier);

        Console.WriteLine($"Success: {result.Success}, Amount: {result.RebateAmount}");
        Console.WriteLine($"Stored Rebate Amount: {rebateStored.Amount}");
    }
}
