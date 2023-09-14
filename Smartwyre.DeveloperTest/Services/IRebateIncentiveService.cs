using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateIncentiveService
{
    public CalculateRebateResult CheckIncentive(CheckIncentiveDto dto);

    public class CheckIncentiveDto
    {
        public Rebate rebate { get; set; } = null;

        public Product product { get; set; } = null;

        public CalculateRebateRequest request { get; set; } = null;
    }

}
