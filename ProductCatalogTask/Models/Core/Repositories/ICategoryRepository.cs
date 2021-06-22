using ProductCatalogTask.Models.Core.Domain;
using System.Collections;
using System.Collections.Generic;

namespace ProductCatalogTask.Models.Core.Repositories
{
    public interface ICategoryRepository:IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesWithProducts();
        IEnumerable<Category> GetProductCategories(int productId);
    }
}
