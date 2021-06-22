using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductCatalogTask.Models.Core.Domain
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:n}")]
        [Required]
        public float Price { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}