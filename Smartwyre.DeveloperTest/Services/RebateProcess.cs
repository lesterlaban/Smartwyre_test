using System;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public abstract class RebateProcess
{
    protected Rebate _rebate;
    protected CalculateRebateRequest _request;
    protected SupportedIncentiveType _supportedIncentiveType;
    public static RebateProcess Create(Rebate rebate, CalculateRebateRequest request)
    {
        return rebate?.Incentive switch
        {
            IncentiveType.FixedCashAmount => new RebateCash(rebate, request),
            IncentiveType.FixedRateRebate => new RebateRate(rebate, request),
            IncentiveType.AmountPerUom => new RebateAmountPerUom(rebate, request),
            _ => throw new MissingMemberException($"No valid incentive found."),
        };
    }

    public virtual bool Validate(Product product) 
    { 
        if(_rebate == null || product == null || !product.SupportedIncentives.HasFlag(_supportedIncentiveType))
            return false;
        return true;
    }

    public virtual decimal Calculate(Product product) => 0m;
}


public class RebateCash : RebateProcess
{
    public RebateCash(Rebate rebate, CalculateRebateRequest request)
    { 
        _rebate = rebate;
        _request = request;
        _supportedIncentiveType = SupportedIncentiveType.FixedCashAmount;
    }
    public override bool Validate(Product product)
    {
        if(!base.Validate(product) || _rebate.Amount == 0)
            return false;

        return true;
    }
    public override decimal Calculate(Product product) => _rebate.Amount;

}

public class RebateRate : RebateProcess
{
    public RebateRate(Rebate rebate, CalculateRebateRequest request)
    {
        _rebate = rebate;
        _request = request;
        _supportedIncentiveType = SupportedIncentiveType.FixedRateRebate;
    }
    public override bool Validate(Product product)
    {
        if(!base.Validate(product) || _rebate.Percentage == 0 || product.Price == 0 || _request.Volume == 0)
            return false;
        return true;
    }
    public override decimal Calculate(Product product) => product.Price * _rebate.Percentage * _request.Volume;

}

public class RebateAmountPerUom : RebateProcess
{
    public RebateAmountPerUom(Rebate rebate, CalculateRebateRequest request)
    {
        _rebate = rebate;
        _request = request;
        _supportedIncentiveType = SupportedIncentiveType.AmountPerUom;
    }
    public override bool Validate(Product product)
    {
        if(!base.Validate(product) || _rebate.Amount == 0 || _request.Volume == 0)
            return false;
        return true;
    }

    public override decimal Calculate(Product product) => _rebate.Amount * _request.Volume;

}