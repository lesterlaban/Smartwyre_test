using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public interface IProductDataStore
{
    /// <summary>
    /// Method to get product
    /// </summary>
    /// <param name="productIdentifier">id to get product</param>
    /// <returns>product</returns>
    public Product GetProduct(string productIdentifier);
}
