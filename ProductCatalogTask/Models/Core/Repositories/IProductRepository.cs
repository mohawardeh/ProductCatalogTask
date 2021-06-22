using ProductCatalogTask.Models.Core.Domain;
using System.Collections.Generic;

namespace ProductCatalogTask.Models.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsWithCategories();
        IEnumerable<Product> GetCategoryFullProducts(IEnumerable<int> categories);
        IEnumerable<Product> GetCategoryProducts(int categoryId);
        Product GetFullProduct(int id);
    }
}
