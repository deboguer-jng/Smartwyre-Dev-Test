using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;
public class RebateIncentiveServiceTests : TestBase
{
    [Fact]
    public void CheckIncentive_FixedCashAmount_Success()
    {
        var sp = BuildServiceProvider();

        var rebateIncentiveService = sp.GetRequiredService<IRebateIncentiveService>();
        var rebateStoreService = sp.GetRequiredService<IRebateDataStore>();

        var product = new Product()
        {
            Identifier = "product1",
            SupportedIncentive = IncentiveType.FixedCashAmount,
            Uom = "uom",
            Price = 123,
            Id = 0
        };

        var rebate = new Rebate()
        {
            Identifier = "rebate1",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 101,
            Percentage = 10
        };

        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "product1",
            RebateIdentifier = "rebate1",
            Volume = 123
        };

        var result = rebateIncentiveService.CheckIncentive(new IRebateIncentiveService.CheckIncentiveDto()
        {
            rebate = rebate,
            product = product,
            request = request
        }) ;
        var rebateStore = rebateStoreService.GetRebate(rebate.Identifier);

        Assert.True(result.Success);
        Assert.Equal(101, result.RebateAmount);
    }

    [Fact]
    public void CheckIncentive_FixedCashAmount_Fail_WhenRebateOrProductIsNull()
    {
        var sp = BuildServiceProvider();

        var rebateIncentiveService = sp.GetRequiredService<IRebateIncentiveService>();

        var product = new Product()
        {
            Identifier = "product1",
            SupportedIncentive = IncentiveType.FixedCashAmount,
            Uom = "uom",
            Price = 123,
            Id = 0
        };

        var rebate = new Rebate()
        {
            Identifier = "rebate1",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 100,
            Percentage = 10
        };

        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "product1",
            RebateIdentifier = "rebate1",
            Volume = 123
        };

        var resultOne = rebateIncentiveService.CheckIncentive(new IRebateIncentiveService.CheckIncentiveDto() {
            product = product, 
            request = request
        });

        var resultTwo = rebateIncentiveService.CheckIncentive(new IRebateIncentiveService.CheckIncentiveDto()
        {
            rebate = rebate,
            request = request
        });

        Assert.False(resultOne.Success);
        Assert.Equal(0, resultOne.RebateAmount);

        Assert.False(resultTwo.Success);
        Assert.Equal(0, resultTwo.RebateAmount);
    }

    [Fact]
    public void CheckIncentive_FixedCashAmount_Fail_WhenProductIncentiveIsNotSupported()
    {
        var sp = BuildServiceProvider();

        var rebateIncentiveService = sp.GetRequiredService<IRebateIncentiveService>();

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
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 100,
            Percentage = 10
        };

        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "product1",
            RebateIdentifier = "rebate1",
            Volume = 123
        };

        var resultOne = rebateIncentiveService.CheckIncentive(new IRebateIncentiveService.CheckIncentiveDto()
        {
            rebate = rebate,
            product = product,
            request = request
        });

        Assert.False(resultOne.Success);
        Assert.Equal(0, resultOne.RebateAmount);
    }

    [Fact]
    public void CheckIncentive_FixedCashAmount_Fail_WhenRebateAmountIsZero()
    {
        var sp = BuildServiceProvider();

        var rebateIncentiveService = sp.GetRequiredService<IRebateIncentiveService>();

        var product = new Product()
        {
            Identifier = "product1",
            SupportedIncentive = IncentiveType.FixedCashAmount,
            Uom = "uom",
            Price = 123,
            Id = 0
        };

        var rebate = new Rebate()
        {
            Identifier = "rebate1",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 0,
            Percentage = 10
        };

        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "product1",
            RebateIdentifier = "rebate1",
            Volume = 123
        };

        var resultOne = rebateIncentiveService.CheckIncentive(new IRebateIncentiveService.CheckIncentiveDto()
        {
            rebate = rebate,
            product = product,
            request = request
        });

        Assert.False(resultOne.Success);
        Assert.Equal(0, resultOne.RebateAmount);
    }
}
