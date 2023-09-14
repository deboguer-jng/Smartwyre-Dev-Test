using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductDataStore
{
    public Dictionary<string, Product> products;

    public ProductDataStore()
    {
        products = new Dictionary<string, Product>();
    }

    public void SetProduct(string productIdentifier, Product product)
    {
        products.Add(productIdentifier, product);
    }

    public Product GetProduct(string productIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 

        return products.GetValueOrDefault(productIdentifier);
    }
}
