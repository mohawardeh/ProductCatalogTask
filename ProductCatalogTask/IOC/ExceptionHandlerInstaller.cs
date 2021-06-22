using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ProductCatalogTask.Filters;

namespace ProductCatalogTask.IOC
{
    public class ExceptionHandlerInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ExceptionHandler>().LifestyleTransient()
            );

        }
    }
}