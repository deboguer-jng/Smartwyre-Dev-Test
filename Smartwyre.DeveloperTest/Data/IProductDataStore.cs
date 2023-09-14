using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;
public interface IProductDataStore
{
    public void SetProduct(string productIdentifier, Product product);
    public Product GetProduct(string productIdentifier);
}
