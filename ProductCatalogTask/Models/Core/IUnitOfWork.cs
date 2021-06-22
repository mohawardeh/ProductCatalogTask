using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalogTask.Models.Core.Repositories;
namespace ProductCatalogTask.Models.Core
{
    public interface IUnitOfWork:IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }

        IExceptionRepository Exceptions { get; }
        int Complete();
    }
}
