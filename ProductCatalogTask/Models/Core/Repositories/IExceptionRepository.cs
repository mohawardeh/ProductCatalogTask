using ProductCatalogTask.Models.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogTask.Models.Core.Repositories
{
    public interface IExceptionRepository: IRepository<ExceptionRecord>
    {
    }
}
