using ProductCatalogTask.Models.Core;
using ProductCatalogTask.Models.Core.Repositories;
using ProductCatalogTask.Models.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductCatalogTask.Models.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
            Exceptions = new ExceptionRepository(_context);
        }

        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IExceptionRepository Exceptions { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}