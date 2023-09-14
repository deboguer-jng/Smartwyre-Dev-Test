using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _dataStore;

    private readonly IProductDataStore _productStore;

    private readonly IRebateIncentiveService _rebateIncentiveService;

    public RebateService(IRebateDataStore dataStore, IProductDataStore productStore, IRebateIncentiveService rebateIncentiveService)
    {
        _dataStore = dataStore;
        _productStore = productStore;
        _rebateIncentiveService = rebateIncentiveService;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = _dataStore.GetRebate(request.RebateIdentifier);
        Product product = _productStore.GetProduct(request.ProductIdentifier);

        CalculateRebateResult result = _rebateIncentiveService.CheckIncentive(
            new IRebateIncentiveService.CheckIncentiveDto() {
                rebate = rebate, 
                product = product,
                request = request
            }
        );

        if (result.Success)
        {
            _dataStore.StoreCalculationResult(rebate.Identifier, result.RebateAmount);
        }

        return result;
    }
}
