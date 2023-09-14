using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Tests;

public class TestBase
{
    private bool _spCreated;
    private IServiceProvider serviceProvider;

    protected IServiceProvider BuildServiceProvider()
    {
        if (_spCreated)
        {
            return serviceProvider;
        }

        serviceProvider = new ServiceCollection()
            .AddSingleton<IProductDataStore, ProductDataStore>()
            .AddSingleton<IRebateDataStore, RebateDataStore>()
            .AddSingleton<IRebateIncentiveService, RebateIncentiveService>()
            .AddSingleton<IRebateService, RebateService>()
            .BuildServiceProvider();

        var productStore = serviceProvider.GetRequiredService<IProductDataStore>();
        var rebateStore = serviceProvider.GetRequiredService<IRebateDataStore>();

        BuildProductStore(productStore);
        BuildRebateStore(rebateStore);

        _spCreated = true;

        return serviceProvider;
    }

    protected virtual void BuildProductStore(IProductDataStore productStore)
    {
        var product1 = new Product()
        {
            Identifier = "product1",
            SupportedIncentive = IncentiveType.FixedCashAmount,
            Uom = "uom",
            Price = 123,
            Id = 0
        };

        productStore.SetProduct("product1", product1);
    }

    protected virtual void BuildRebateStore(IRebateDataStore rebateStore)
    {
        var rebate = new Rebate()
        {
            Identifier = "rebate1",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 100,
            Percentage = 10
        };

        rebateStore.SetRebate("rebate1", rebate);
    }
}
