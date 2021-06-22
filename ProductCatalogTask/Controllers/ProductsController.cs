using ProductCatalogTask.Models.Core;
using ProductCatalogTask.Models.Core.Domain;
using ProductCatalogTask.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCatalogTask.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        
        public ProductsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: Products
        [AllowAnonymous]
        public ActionResult Index(int[] categories)
        {
            var viewModel = new ProductListViewModel();
            var dbCategories = unitOfWork.Categories.GetAll();
            if (categories == null)
            {
                var products = unitOfWork.Products.GetProductsWithCategories();

                viewModel.Products = products;
                viewModel.Categories = new SelectList(dbCategories, "Id", "Name");
            }
            else
            {
                var products = unitOfWork.Products.GetCategoryFullProducts(categories);
                var categorySelectList = new List<SelectListItem>();
                foreach (var category in dbCategories)
                {
                    var item = new SelectListItem() { Value = category.Id.ToString(), Text = category.Name, Selected = categories.Contains(category.Id) };
                    categorySelectList.Add(item);
                }
                viewModel.Products = products;
                viewModel.Categories = categorySelectList;
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateOrEdit(int? id)
        {
            var viewModel = new ProductViewModel();
            var dbCategories = unitOfWork.Categories.GetAll();
            if (id == null)
            {
                viewModel.Categories = new SelectList(dbCategories, "Id", "Name");
                return View(viewModel);
            }
            else
            {
                var product = unitOfWork.Products.GetFullProduct((int)id);
                viewModel.Product = product;
                var categorySelectList = new List<SelectListItem>();
                foreach (var category in dbCategories)
                {
                    var item = new SelectListItem() { Value = category.Id.ToString(), Text = category.Name, Selected = product.Categories.Contains(category) };
                    categorySelectList.Add(item);
                }
                viewModel.Categories = categorySelectList;
                return View(viewModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateOrEdit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Product.Id == 0)
                {
                    var dbProducts = unitOfWork.Products.Find(p => p.Name.Equals(model.Product.Name));
                    if (dbProducts.Count() == 0)
                    {
                        model.Product.Categories = new List<Category>();
                        if (model.ChosenCategories != null)
                            model.Product.Categories = unitOfWork.Categories.Find(c => model.ChosenCategories.Contains(c.Id)).ToList();
                        unitOfWork.Products.Add(model.Product);
                        unitOfWork.Complete();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Product already exists");
                }
                else
                {
                    var dbProduct = unitOfWork.Products.GetFullProduct(model.Product.Id);
                    var dbProducts = unitOfWork.Products.Find(p => p.Name.Equals(model.Product.Name) && p.Id!= model.Product.Id);
                    if (dbProduct != null && dbProducts.Count()==0)
                    {
                        dbProduct.Name = model.Product.Name;
                        dbProduct.Description = model.Product.Description;
                        dbProduct.Price = model.Product.Price;
                        if (model.ChosenCategories != null)
                            dbProduct.Categories = unitOfWork.Categories.Find(c => model.ChosenCategories.Contains(c.Id)).ToList();
                        else
                            dbProduct.Categories = null;
                        unitOfWork.Products.Update(dbProduct);
                        unitOfWork.Complete();
                        return RedirectToAction("Index");
                    }
                    else
                    {                        
                        if(dbProduct==null)
                            return HttpNotFound();
                        else 
                            ModelState.AddModelError("", "New Product already exists");
                    }
                }
            }
            var dbCategories = unitOfWork.Categories.GetAll();
            var categorySelectList = new List<SelectListItem>();
            foreach (var category in dbCategories)
            {
                var item = new SelectListItem() { Value = category.Id.ToString(), Text = category.Name, Selected = (model.ChosenCategories != null) ? model.ChosenCategories.Contains(category.Id) : false };
                categorySelectList.Add(item);
            }
            model.Categories = categorySelectList;
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var dbProduct = unitOfWork.Products.Get(id);
            if (dbProduct != null)
            {
                unitOfWork.Products.Remove(dbProduct);
                unitOfWork.Complete();
                return RedirectToAction("Index", "Products");
            }
            return HttpNotFound();
        }
    }
}