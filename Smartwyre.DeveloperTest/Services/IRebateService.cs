using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateService
{
    /// <summary>
    /// MEthod to calculare rebate
    /// </summary>
    /// <param name="request">request</param>
    /// <returns>result</returns> <summary>
    CalculateRebateResult Calculate(CalculateRebateRequest request);
}
