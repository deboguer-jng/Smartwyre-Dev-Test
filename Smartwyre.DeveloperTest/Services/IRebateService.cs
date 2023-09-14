using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateService
{
    public CalculateRebateResult Calculate(CalculateRebateRequest request);
}
