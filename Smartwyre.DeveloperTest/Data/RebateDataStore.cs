using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateDataStore
{
    public Dictionary<string, Rebate> rebates;

    public RebateDataStore()
    {
        rebates = new Dictionary<string, Rebate>();
    }

    public void SetRebate(string rebateIdentifier, Rebate rebate)
    {
        rebates.Add(rebateIdentifier, rebate);
    }

    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 
        return rebates.GetValueOrDefault(rebateIdentifier);
    }

    public void StoreCalculationResult(string rebateIdentifier, decimal rebateAmount)
    {
        // Update account in database, code removed for brevity

        var rebate = rebates.GetValueOrDefault(rebateIdentifier);

        rebate.Amount = rebateAmount;
    }
}
