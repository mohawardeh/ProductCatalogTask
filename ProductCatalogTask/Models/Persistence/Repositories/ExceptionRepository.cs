using ProductCatalogTask.Models.Core.Domain;
using ProductCatalogTask.Models.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductCatalogTask.Models.Persistence.Repositories
{
    public class ExceptionRepository: Repository<ExceptionRecord>, IExceptionRepository
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public ExceptionRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}