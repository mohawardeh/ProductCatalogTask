using ProductCatalogTask.Models.Core;
using ProductCatalogTask.Models.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCatalogTask.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: Categories
        [AllowAnonymous]
        public ActionResult Index()
        {
            var categories = unitOfWork.Categories.GetAll();
            return View(categories);
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(int id)
        {
            var dbCategory = unitOfWork.Categories.Get(id);
            if (dbCategory != null)
            {
                unitOfWork.Categories.Remove(dbCategory);
                unitOfWork.Complete();
                return RedirectToAction("Index", "Categories");
            }
            return HttpNotFound();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CreateOrEdit(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                var category = unitOfWork.Categories.SingleOrDefault(c => c.Id == id);
                if (category == null)
                    return HttpNotFound();
                return View(category);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateOrEdit(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    var dbCategories = unitOfWork.Categories.Find(c => c.Name.Equals(category.Name)).ToList();
                    if (dbCategories.Count == 0)
                    {
                        unitOfWork.Categories.Add(category);
                        unitOfWork.Complete();
                        return RedirectToAction("Index", "Categories");
                    }
                    ModelState.AddModelError("", "Category already exists");
                }
                else
                {
                    var dbCategory = unitOfWork.Categories.Get(category.Id);
                    if (dbCategory != null)
                    {
                        if (!category.Name.Equals(dbCategory.Name))
                        {
                            dbCategory.Name = category.Name;
                            dbCategory.Description = category.Description;
                            unitOfWork.Categories.Update(dbCategory);
                            unitOfWork.Complete();
                            return RedirectToAction("Index", "Categories");
                        }
                        ModelState.AddModelError("", "New Category already exists");                        
                    }
                }
            }
            return View(category);
        }
    }
}