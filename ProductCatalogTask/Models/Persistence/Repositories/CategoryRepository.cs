using ProductCatalogTask.Models.Core.Domain;
using ProductCatalogTask.Models.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace ProductCatalogTask.Models.Persistence.Repositories
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Category> GetCategoriesWithProducts()
        {
            return ((ApplicationDbContext)Context).Categories.Include(c=>c.Products).ToList();
        }
        public IEnumerable<Category> GetProductCategories(int productId)
        {
            return ((ApplicationDbContext)Context).Categories.Where(p => p.Products.Any(c => c.Id == productId)).ToList();
        }
    }
}