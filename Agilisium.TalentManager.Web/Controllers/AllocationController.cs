using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using Agilisium.TalentManager.Web.Helpers;
using Agilisium.TalentManager.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class AllocationController : Controller
    {
        private readonly IEmployeeService empService;
        private readonly IDropDownSubCategoryService subCategoryService;
        private readonly IProjectService projectService;
        private readonly IAllocationService allocationService;

        public AllocationController(IEmployeeService empService,
            IDropDownSubCategoryService subCategoryService,
            IAllocationService allocationService, IProjectService projectService)
        {
            this.empService = empService;
            this.subCategoryService = subCategoryService;
            this.allocationService = allocationService;
            this.projectService = projectService;
        }

        // GET: Project
        public ActionResult Index()
        {
            IEnumerable<AllocationViewModel> allocations = GetAllocations();
            return View(allocations);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            InitializeAddUpdatePageData();
            return View(new AllocationViewModel());
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(AllocationViewModel allocation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (allocationService.Exists(allocation.EmployeeID, allocation.ProjectID))
                    {
                        ModelState.AddModelError("", $"I guess, the project {allocation.ProjectName} is already allocated to {allocation.EmployeeName}");
                        return View(allocation);
                    }

                    ProjectAllocationDto projectDto = Mapper.Map<AllocationViewModel, ProjectAllocationDto>(allocation);
                    allocationService.Add(projectDto);
                    TempData["AlertMessage"] = $"New project allocation has been created for {allocation.EmployeeName}";
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return RedirectToAction("Index");
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            AllocationViewModel empModel = new AllocationViewModel();
            try
            {
                if (!id.HasValue)
                {
                    ModelState.AddModelError("", "Looks like, the required data is not available with your request");
                    return View(new AllocationViewModel());
                }

                ProjectAllocationDto emp = allocationService.GetByID(id.Value);

                if (emp == null)
                {
                    ModelState.AddModelError("", $"Sorry, we couldn't find the allocation details with ID: {id.Value}");
                    return View(new AllocationViewModel());
                }
                empModel = Mapper.Map<ProjectAllocationDto, AllocationViewModel>(emp);
                InitializeAddUpdatePageData();
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(empModel);
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(AllocationViewModel allocation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (allocationService.Exists(allocation.AllocationEntryID, allocation.EmployeeID, allocation.ProjectID))
                    {
                        ModelState.AddModelError("", $"I guess, the project {allocation.ProjectName} is already allocated to {allocation.EmployeeName}");
                        return View(allocation);
                    }

                    ProjectAllocationDto projectDto = Mapper.Map<AllocationViewModel, ProjectAllocationDto>(allocation);
                    allocationService.Update(projectDto);
                    TempData["AlertMessage"] = $"Project allocation details have been updated for {allocation.EmployeeName}";
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return RedirectToAction("Index");
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                ModelState.AddModelError("", "Looks like, the Allocation ID is missing from your request");
                return View(new AllocationViewModel());
            }

            try
            {
                allocationService.Delete(new ProjectAllocationDto { AllocationEntryID = id.Value });
                TempData["AlertMessage"] = "Allocation details have been removed successfully";
                return RedirectToAction("Index");
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
                return View();
            }
        }

        private IEnumerable<AllocationViewModel> GetAllocations()
        {
            IEnumerable<ProjectAllocationDto> allocations = allocationService.GetAll();
            IEnumerable<AllocationViewModel> projectModels = Mapper.Map<IEnumerable<ProjectAllocationDto>, IEnumerable<AllocationViewModel>>(allocations);
            CacheHelper.AddOrUpdateItem(UIConstants.ALLOCATION_MODELS_LIST, projectModels, HttpContext);
            return projectModels;
        }

        private void InitializeAddUpdatePageData()
        {
            ViewData["IsNewProject"] = true;
            PrepareSubCategoriesDDItems();
            ViewData["EmployeesDDList"] = GetEmployeesDDList();
        }

        private void PrepareSubCategoriesDDItems()
        {
            IEnumerable<DropDownSubCategoryDto> buList = subCategoryService.GetAll();

            List<SelectListItem> projectTypeItems = (from c in buList
                                                     orderby c.SubCategoryName
                                                     where c.CategoryID == (int)CategoryType.UtilizationCode
                                                     select new SelectListItem
                                                     {
                                                         Text = c.SubCategoryName,
                                                         Value = c.SubCategoryID.ToString()
                                                     }).ToList();

            projectTypeItems.Insert(0, new SelectListItem
            {
                Text = "Please Select",
                Value = "0",
            });

            ViewData["UtilizationTypeDDList"] = projectTypeItems;
        }

        private List<SelectListItem> GetEmployeesDDList()
        {
            List<EmployeeViewModel> employeeModels = (List<EmployeeViewModel>)CacheHelper.GetItem(UIConstants.EMPLOYEE_MODELS_LIST, HttpContext);

            if (employeeModels == null)
            {
                List<EmployeeDto> employees = empService.GetAllEmployees();
                employeeModels = Mapper.Map<List<EmployeeDto>, List<EmployeeViewModel>>(employees);
                CacheHelper.AddOrUpdateItem(UIConstants.EMPLOYEE_MODELS_LIST, employeeModels, HttpContext);
            }

            List<SelectListItem> empDDList = new List<SelectListItem>
            {
                new SelectListItem{Text = "Please Select", Value = string.Empty}
            };

            if (employeeModels != null)
            {
                foreach (EmployeeViewModel item in employeeModels)
                {
                    empDDList.Add(new SelectListItem
                    {
                        Text = $"{item.LastName}, {item.FirstName}",
                        Value = item.EmployeeEntryID.ToString()
                    });
                }
            }

            return empDDList;
        }
    }
}
