using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ProductCatalogTask.Models.Core;
using ProductCatalogTask.Models.Persistence;
using ProductCatalogTask.Models.Persistence.Repositories;
using ProductCatalogTask.Models.Core.Repositories;
namespace ProductCatalogTask.IOC
{
    public class DataAccessLayerInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ApplicationDbContext>().LifestyleTransient()
            );
            container.Register(
            Component.For<IUnitOfWork>()
            .ImplementedBy<UnitOfWork>().LifestyleTransient()
            );
            //container.Register(
            //Component.For<ICategoryRepository>()
            //.ImplementedBy<CategoryRepository>()
            //);
            //container.Register(
            //Component.For<IProductRepository>()
            //.ImplementedBy<ProductRepository>()
            //);
            //container.Register(
            //Component.For<IProductCategoryRepository>()
            //.ImplementedBy<ProductCategoryRepository>()
            //);
        }
    }
}