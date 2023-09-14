using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;
public interface IRebateDataStore
{
    public void SetRebate(string rebateIdentifier, Rebate rebate);

    public Rebate GetRebate(string rebateIdentifier);

    public void StoreCalculationResult(string rebateIdentifier, decimal rebateAmount);
}
