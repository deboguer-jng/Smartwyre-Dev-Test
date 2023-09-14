using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateIncentiveService : IRebateIncentiveService
{
    private static CalculateRebateResult CheckFixedCashAmountIncentive(Rebate rebate, Product product)
    {
        var result = new CalculateRebateResult();

        if (!product.SupportedIncentive.HasFlag(IncentiveType.FixedCashAmount))
        {
            result.Success = false;
        }
        else if (rebate.Amount == 0)
        {
            result.Success = false;
        }
        else
        {
            result.RebateAmount = rebate.Amount;
            result.Success = true;
        }

        return result;
    }

    private static CalculateRebateResult CheckFixedRateRebateIncentive(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        var result = new CalculateRebateResult();
        
        if (!product.SupportedIncentive.HasFlag(IncentiveType.FixedRateRebate))
        {
            result.Success = false;
        }
        else if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
        {
            result.Success = false;
        }
        else
        {
            result.RebateAmount += product.Price * rebate.Percentage * request.Volume;
            result.Success = true;
        }

        return result;
    }

    private static CalculateRebateResult CheckAmountPerUomIncentive(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        var result = new CalculateRebateResult();

        if (!product.SupportedIncentive.HasFlag(IncentiveType.AmountPerUom))
        {
            result.Success = false;
        }
        else if (rebate.Amount == 0 || request.Volume == 0)
        {
            result.Success = false;
        }
        else
        {
            result.RebateAmount += rebate.Amount * request.Volume;
            result.Success = true;
        }

        return result;
    }

    public CalculateRebateResult CheckIncentive(IRebateIncentiveService.CheckIncentiveDto dto)
    {
        if (dto.rebate == null || dto.product == null)
        {
            return new CalculateRebateResult() { Success = false };
        }

        return dto.rebate.Incentive switch
        {
            IncentiveType.FixedCashAmount => CheckFixedCashAmountIncentive(dto.rebate, dto.product),
            IncentiveType.FixedRateRebate => CheckFixedRateRebateIncentive(dto.rebate, dto.product, dto.request),
            _ => CheckAmountPerUomIncentive(dto.rebate, dto.product, dto.request),
        };
    }
}

