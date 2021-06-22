using ProductCatalogTask.Models.Core;
using ProductCatalogTask.Models.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductCatalogTask.Controllers.API
{
    public class CategoriesController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET /api/categories
        public IEnumerable<Category> GetCategories()
        {
            return unitOfWork.Categories.GetAll();
        }

        // GET /api/categories/id
        public Category GetCategory(int id)
        {
            var category = unitOfWork.Categories.Get(id);
            if (category == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return category;
        }

        // POST /api/categories
        [HttpPost]
        [Authorize]
        public Category CreateCategory(Category category)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            unitOfWork.Categories.Add(category);
            unitOfWork.Complete();
            return category;
        }

        // PUT /api/categories/1
        [HttpPut]
        [Authorize]
        public void UpdateCategory(int id,Category category)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var dbCategory = unitOfWork.Categories.SingleOrDefault(s => s.Id == id);
            if(dbCategory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            dbCategory.Name = category.Name;
            dbCategory.Description = category.Description;
            unitOfWork.Categories.Update(dbCategory);
            unitOfWork.Complete();
        }

        // DELETE /api/categories/1
        [HttpDelete]
        [Authorize]
        public void DeleteCategory(int id) 
        {
            var dbCategory = unitOfWork.Categories.SingleOrDefault(s => s.Id == id);
            if (dbCategory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            unitOfWork.Categories.Remove(dbCategory);
            unitOfWork.Complete();
        }
    }
}
