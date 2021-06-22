using ProductCatalogTask.Models.Core.Domain;
using ProductCatalogTask.Models.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace ProductCatalogTask.Models.Persistence.Repositories
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public ProductRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Product> GetProductsWithCategories()
        {
            return ((ApplicationDbContext)Context).Products.Include(p => p.Categories).ToList();
        }

        public IEnumerable<Product> GetCategoryProducts(int categoryId)
        {
            return ((ApplicationDbContext)Context).Products.Where(p => p.Categories.Any(c => c.Id == categoryId)).ToList();
        }
        public IEnumerable<Product> GetCategoryFullProducts(IEnumerable<int> categories)
        {
            return ((ApplicationDbContext)Context).Products.Where(p => p.Categories.Any(c => categories.Contains(c.Id))).Include(p => p.Categories);
        }

        public Product GetFullProduct(int id)
        {
            return ((ApplicationDbContext)Context).Products.Include(p=>p.Categories).SingleOrDefault(p=>p.Id==id);
        }
    }
}