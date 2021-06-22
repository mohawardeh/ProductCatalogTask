using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductCatalogTask.Models.Core.Domain
{
    public class ExceptionRecord
    {

        public int Id { get; set; }


        [StringLength(50)]
        public string UserName { get; set; }


        [StringLength(50)]
        public string ActionName { get; set; }


        [StringLength(50)]
        public string ControllerName { get; set; }


        public string Parameters { get; set; }


        public DateTime ExceptionDate { get; set; }


        public string ExceptionMessage { get; set; }
    }
}