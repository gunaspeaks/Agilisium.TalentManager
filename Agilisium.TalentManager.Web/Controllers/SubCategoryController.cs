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
    public class SubCategoryController : Controller
    {
        private readonly IDropDownSubCategoryService subCategoryService;
        private readonly IDropDownCategoryService categoryService;

        public SubCategoryController(IDropDownCategoryService categoryService, IDropDownSubCategoryService subCategoryService)
        {
            this.categoryService = categoryService;
            this.subCategoryService = subCategoryService;
        }

        // GET: SubCategory/1
        public ActionResult Index(string selectedCategoryID)
        {
            SubCategoryListViewModel model = new SubCategoryListViewModel
            {
                Categories = GetCategoriesDropDownList()
            };

            if (string.IsNullOrEmpty(selectedCategoryID))
            {
                model.SelectedCategoryID = int.Parse(model.Categories.First().Value);
            }
            else
            {
                model.SelectedCategoryID = int.Parse(model.Categories.First(c => c.Value == selectedCategoryID.ToString())?.Value);
            }

            Session["SelectedCategoryID"] = model.SelectedCategoryID.ToString();
            model.SubCategories = GetSubCategories(model.SelectedCategoryID).ToList();
            return View(model);
        }

        // GET: SubCategory/Create
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> listItems = GetCategoriesDropDownList();
            ViewData["CategoriesList"] = listItems;
            return View(new SubCategoryViewModel());
        }

        // POST: SubCategory/Create
        [HttpPost]
        public ActionResult Create(SubCategoryViewModel subCategory)
        {
            try
            {
                IEnumerable<SelectListItem> listItems = GetCategoriesDropDownList();
                ViewData["CategoriesList"] = listItems;

                if (ModelState.IsValid)
                {
                    if (subCategoryService.Exists(subCategory.SubCategoryName))
                    {
                        ModelState.AddModelError("", "This Sub-Category Name is duplicate");
                        return View(subCategory);
                    }
                    DropDownSubCategoryDto subCategoryModel = Mapper.Map<SubCategoryViewModel, DropDownSubCategoryDto>(subCategory);
                    subCategoryService.CreateSubCategory(subCategoryModel);
                    TempData["AlertMessage"] = "New Sub-Category has been stored successfully";
                    Session["SelectedCategoryID"] = subCategory.CategoryID.ToString();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(subCategory);
        }

        // GET: SubCategory/Edit/5
        public ActionResult Edit(int id)
        {
            DropDownSubCategoryDto category = subCategoryService.GetSubCategory(id);
            SubCategoryViewModel uiCategory = Mapper.Map<DropDownSubCategoryDto, SubCategoryViewModel>(category);
            IEnumerable<SelectListItem> listItems = GetCategoriesDropDownList();
            ViewData["CategoriesList"] = listItems;
            return View(uiCategory);
        }

        // POST: SubCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SubCategoryViewModel subCategoryModel)
        {
            try
            {
                IEnumerable<SelectListItem> listItems = GetCategoriesDropDownList();
                ViewData["CategoriesList"] = listItems;

                if (ModelState.IsValid || (!ModelState.IsValid && ModelState.Values.Count(p => p.Errors.Count > 0) == 1))
                {
                    if (subCategoryService.Exists(subCategoryModel.SubCategoryName, subCategoryModel.SubCategoryID))
                    {
                        ModelState.AddModelError("", "This Sub-Category Name is duplicate");
                        return View(subCategoryModel);
                    }

                    DropDownSubCategoryDto subCategory = Mapper.Map<SubCategoryViewModel, DropDownSubCategoryDto>(subCategoryModel);
                    subCategoryService.UpdateSubCategory(subCategory);
                    TempData["AlertMessage"] = "Sub-Category has been updated successfully";
                    Session["SelectedCategoryID"] = subCategoryModel.CategoryID.ToString();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(subCategoryModel);
        }

        // GET: SubCategory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                subCategoryService.DeleteSubCategory(new DropDownSubCategoryDto { SubCategoryID = id });
                TempData["AlertMessage"] = "Sub-Category has been deleted successfully";
                int categoryID = int.Parse(Session["SelectedCategoryID"].ToString());
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Oops! an error has occured while deleting");
            }
            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> GetCategoriesDropDownList()
        {
            List<DropDownCategoryDto> categories = categories = categoryService.GetCategories().ToList();
            List<SelectListItem> categoriesList = (from cat in categories
                                                   orderby cat.CategoryName
                                                   select new SelectListItem
                                                   {
                                                       Text = cat.CategoryName,
                                                       Value = $"{cat.CategoryID}"
                                                   }).ToList();
            if (categoriesList != null && categoriesList.Count > 0)
            {
                return categoriesList;
            }

            return new List<SelectListItem>
            {
                new SelectListItem{Text = "None", Value = "0"}
            };
        }

        private IEnumerable<SubCategoryViewModel> GetSubCategories(int categoryID)
        {
            IEnumerable<DropDownSubCategoryDto> subCategories = subCategoryService.GetSubCategories(categoryID)?.OrderBy(p => p.SubCategoryName);
            IEnumerable<SubCategoryViewModel> uiCategories = Mapper.Map<IEnumerable<DropDownSubCategoryDto>, IEnumerable<SubCategoryViewModel>>(subCategories);
            return uiCategories;
        }
    }
}
