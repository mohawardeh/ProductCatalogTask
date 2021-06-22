using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProductCatalogTask.Models.Persistence
{
    public class ProductCatalogDatabaseInitializer:CreateDatabaseIfNotExists<ApplicationDbContext>
    {

        protected override  void Seed(ApplicationDbContext context)
        {
            var userstore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userstore);
            var user = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
            var result =  userManager.Create(user, "admin12");
            if (result.Succeeded)
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                roleManager.Create(new IdentityRole("Admin"));
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}