using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using Agilisium.TalentManager.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IDropDownCategoryService service;

        public CategoryController(IDropDownCategoryService categoryService)
        {
            service = categoryService;
        }

        // GET: Category
        public ActionResult Index()
        {
            IEnumerable<CategoryViewModel> categories = GetCategories();
            return View(categories);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(CategoryViewModel category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (service.Exists(category.CategoryName))
                    {
                        ModelState.AddModelError("", "This Category Name is duplicate");
                        return View(category);
                    }
                    DropDownCategoryDto categoryModel = Mapper.Map<CategoryViewModel, DropDownCategoryDto>(category);
                    service.CreateCategory(categoryModel);
                    TempData["AlertMessage"] = "New Category has been stored successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return View("Please select a Category to Edit");
            }
            if (!service.Exists(id.Value))
            {
                return View($"Sorry, We couldn't find the Category with ID: {id.Value}");
            }
            DropDownCategoryDto category = service.GetCategory(id.Value);
            CategoryViewModel uiCategory = Mapper.Map<DropDownCategoryDto, CategoryViewModel>(category);
            return View(uiCategory);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CategoryViewModel category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (service.Exists(category.CategoryName, category.CategoryID))
                    {
                        ModelState.AddModelError("", "This Category Name is duplicate");
                        return View(category);
                    }
                    DropDownCategoryDto categoryModel = Mapper.Map<CategoryViewModel, DropDownCategoryDto>(category);
                    service.UpdateCategory(categoryModel);
                    TempData["AlertMessage"] = "Category has been updated successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.DeleteCategory(id);
                TempData["AlertMessage"] = "Category has been deleted successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private IEnumerable<CategoryViewModel> GetCategories()
        {
            IEnumerable<DropDownCategoryDto> categories = service.GetCategories()?.OrderBy(p => p.CategoryName);
            return Mapper.Map<IEnumerable<DropDownCategoryDto>, IEnumerable<CategoryViewModel>>(categories);
        }
    }
}
