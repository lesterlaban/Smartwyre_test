using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IProductDataStore _productDataStore;
    private readonly IRebateDataStore _rebateDataStore;
    public RebateService(IProductDataStore productDataStore, IRebateDataStore rebateDataStore)
    {
        _productDataStore = productDataStore;
        _rebateDataStore = rebateDataStore;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = _rebateDataStore.GetRebate(request?.RebateIdentifier);
        Product product = _productDataStore.GetProduct(request?.ProductIdentifier);

        var process = RebateProcess.Create(rebate, request);
        var result = new CalculateRebateResult
        {
            Success = process.Validate(product)
        };

        if (result.Success)
        {
            decimal rebateAmount = process.Calculate(product);
            _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
        }

        return result;
    }

}
