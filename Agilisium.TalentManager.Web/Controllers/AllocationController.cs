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
    public class AllocationController : BaseController
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
        public ActionResult List(int page = 1)
        {
            AllocationViewModel viewModel = new AllocationViewModel();
            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = allocationService.TotalRecordsCount(),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.Allocations = GetAllocations(page);
                }
                else
                {
                    DisplayWarningMessage("There are no Project Allocations to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            AllocationModel project = new AllocationModel();

            try
            {
                InitializePageData();
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(project);
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(AllocationModel allocation)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    if (allocation.AllocationEndDate <= allocation.AllocationStartDate)
                    {
                        DisplayWarningMessage("The End date should be greater than the Start date");
                        return View(allocation);
                    }

                    if (allocationService.Exists(allocation.EmployeeID, allocation.ProjectID) > 0)
                    {
                        DisplayWarningMessage($"Looks like the selected project is already allocated to the selected employee");
                        return View(allocation);
                    }

                    ProjectAllocationDto projectDto = Mapper.Map<AllocationModel, ProjectAllocationDto>(allocation);
                    allocationService.Add(projectDto);
                    DisplaySuccessMessage($"New project allocation has been created for {allocation.EmployeeName}");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(allocation);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            AllocationModel empModel = new AllocationModel();

            try
            {
                InitializePageData();

                if (!id.HasValue)
                {
                    DisplayWarningMessage("Looks like, the required data is not available with your request");
                    return RedirectToAction("List");
                }

                if (!allocationService.Exists(id.Value))
                {
                    DisplayWarningMessage($"Sorry, we couldn't find the allocation details with ID: {id.Value}");
                    return RedirectToAction("List");
                }

                ProjectAllocationDto emp = allocationService.GetByID(id.Value);
                empModel = Mapper.Map<ProjectAllocationDto, AllocationModel>(emp);
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }

            return View(empModel);
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(AllocationModel allocation)
        {
            try
            {
                InitializePageData();

                if (ModelState.IsValid)
                {
                    if (allocation.AllocationEndDate <= allocation.AllocationStartDate)
                    {
                        DisplayWarningMessage("The End date should be greater than the Start date");
                        return View(allocation);
                    }

                    if (allocationService.Exists(allocation.AllocationEntryID, allocation.EmployeeID, allocation.ProjectID) > 1)
                    {
                        DisplayWarningMessage($"Looks like the selected project is already allocated to the selected employee");
                        return View(allocation);
                    }

                    ProjectAllocationDto projectDto = Mapper.Map<AllocationModel, ProjectAllocationDto>(allocation);
                    allocationService.Update(projectDto);
                    DisplaySuccessMessage($"Project allocation details have been updated for {allocation.EmployeeName}");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(allocation);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the Allocation ID is missing from your request");
                return RedirectToAction("List");
            }

            try
            {
                allocationService.Delete(new ProjectAllocationDto { AllocationEntryID = id.Value });
                DisplaySuccessMessage("Allocation details have been removed successfully");
                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                DisplayDeleteErrorMessage(exp);
            }

            return RedirectToAction("List");
        }


        [HttpPost]
        public JsonResult GetProjectDetails(int projectID)
        {
            ProjectDto project = projectService.GetByID(projectID);
            JsonResult dat = Json(project);
            return dat;
        }

        [HttpPost]
        public JsonResult GetEmploymentDetails(int empID, int prjID)
        {
            int val = allocationService.GetPercentageOfAllocation(empID, prjID);
            JsonResult res = Json(val);
            return res;
        }

        [HttpPost]
        public JsonResult GetEmployeeOtherAllocations(int empID, int prjID)
        {
            IEnumerable<string> projects = allocationService.GetAllocatedProjectsByEmployeeID(empID, prjID);
            return Json(projects);
        }

        private IEnumerable<AllocationModel> GetAllocations(int pageNo)
        {
            IEnumerable<ProjectAllocationDto> allocations = allocationService.GetAll(RecordsPerPage, pageNo);
            IEnumerable<AllocationModel> projectModels = Mapper.Map<IEnumerable<ProjectAllocationDto>, IEnumerable<AllocationModel>>(allocations);
            return projectModels;
        }

        private void InitializePageData()
        {
            ViewData["IsNewProject"] = true;
            GetOtherDropDownItems();
            GetEmployeesList();
            GetProjectsList();
        }

        private void GetOtherDropDownItems()
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

            ViewBag.ProjectTypeListItems = projectTypeItems;
        }

        private void GetEmployeesList()
        {
            List<EmployeeDto> employees = empService.GetAllEmployees();

            List<SelectListItem> pmList = (from e in employees
                                           select new SelectListItem
                                           {
                                               Text = $"{e.LastName}, {e.FirstName}",
                                               Value = e.EmployeeEntryID.ToString()
                                           }).ToList();

            ViewBag.EmployeeListItems = pmList;
        }

        private void GetProjectsList()
        {
            IEnumerable<ProjectDto> projects = projectService.GetAll();

            List<SelectListItem> projectList = (from p in projects
                                                select new SelectListItem
                                                {
                                                    Text = p.ProjectName,
                                                    Value = p.ProjectID.ToString()
                                                }).ToList();

            ViewBag.ProjectListItems = projectList;
        }
    }
}
