using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using Agilisium.TalentManager.Web.Helpers;
using Agilisium.TalentManager.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IDropDownCategoryService service;

        public CategoryController(IDropDownCategoryService categoryService)
        {
            service = categoryService;
        }

        // GET: Category
        public ActionResult List(int page = 1)
        {
            CategoryViewModel viewModel = new CategoryViewModel();

            try
            {
                viewModel.Categories = GetCategories(page);
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = service.TotalRecordsCount(),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };
            }
            catch (Exception)
            {
                SendLoadErrorMessage();
            }

            return View(viewModel);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(CategoryModel category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (service.Exists(category.CategoryName))
                    {
                        SendWarningMessage($"The Category Name '{category.CategoryName}' is duplicate");
                        return View(category);
                    }
                    DropDownCategoryDto categoryModel = Mapper.Map<CategoryModel, DropDownCategoryDto>(category);
                    service.CreateCategory(categoryModel);
                    SendSuccessMessage($"New Category '{category.CategoryName}' has been stored successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception)
            {
                SendUpdateErrorMessage();
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            CategoryModel uiCategory = new CategoryModel();
            if (!id.HasValue)
            {
                SendWarningMessage("Looks like, the ID is missing in your request");
                return RedirectToAction("List");
            }

            try
            {
                if (!service.Exists(id.Value))
                {
                    SendWarningMessage($"Sorry, We couldn't find the Category with ID: {id.Value}");
                    return RedirectToAction("List");
                }

                DropDownCategoryDto category = service.GetCategory(id.Value);
                uiCategory = Mapper.Map<DropDownCategoryDto, CategoryModel>(category);
                return View(uiCategory);
            }
            catch (Exception)
            {
                SendReadErrorMessage();
            }

            return View(uiCategory);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(CategoryModel category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (service.Exists(category.CategoryName, category.CategoryID))
                    {
                        SendWarningMessage($"Category Name '{category.CategoryName}' is duplicate");
                        return View(category);
                    }
                    DropDownCategoryDto categoryModel = Mapper.Map<CategoryModel, DropDownCategoryDto>(category);
                    service.UpdateCategory(categoryModel);
                    SendSuccessMessage($"Category '{category.CategoryName}' details have been modified successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception)
            {
                SendUpdateErrorMessage();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                SendWarningMessage("Looks like, the ID is missing in your request");
                return RedirectToAction("List");
            }
            try
            {
                if (service.CanBeDeleted(id.Value) == false)
                {
                    SendWarningMessage("There are some dependencies with this Categories. So, you can't delete this for now");
                    return RedirectToAction("List");
                }

                if (service.IsReservedEntry(id.Value))
                {
                    SendWarningMessage("Hey, why do you want to delete a Reserved Category. Please check with the system administrator.");
                    return RedirectToAction("List");
                }

                service.DeleteCategory(new DropDownCategoryDto { CategoryID = id.Value });
                SendSuccessMessage($"Category has been deleted successfully");
            }
            catch (Exception)
            {
                SendDeleteErrorMessage();
            }
            return RedirectToAction("List");
        }

        private IEnumerable<CategoryModel> GetCategories(int pageNo)
        {
            IEnumerable<DropDownCategoryDto> categories = service.GetCategories(RecordsPerPage, pageNo);
            return Mapper.Map<IEnumerable<DropDownCategoryDto>, IEnumerable<CategoryModel>>(categories);
        }
    }
}
