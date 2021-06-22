using ProductCatalogTask.Models.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCatalogTask.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product  Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public int[] ChosenCategories { get; set; }
    }
}