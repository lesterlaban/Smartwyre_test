using Smartwyre.DeveloperTest.Types;
namespace Smartwyre.DeveloperTest.Data;

public interface IRebateDataStore
{
    /// <summary>
    /// Method to get Rebate
    /// </summary>
    /// <param name="rebateIdentifier">ID Rebate</param>
    /// <returns>Rebate</returns>
    public Rebate GetRebate(string rebateIdentifier);
    /// <summary>
    /// Method to caculate store
    /// </summary>
    /// <param name="account">rebate</param>
    /// <param name="rebateAmount">amount rebate</param>
    public void StoreCalculationResult(Rebate account, decimal rebateAmount);
}
