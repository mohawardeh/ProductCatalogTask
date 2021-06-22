using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductCatalogTask.Startup))]
namespace ProductCatalogTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
