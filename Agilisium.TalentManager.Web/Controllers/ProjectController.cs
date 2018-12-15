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
    public class ProjectController : Controller
    {
        private readonly IEmployeeService empService;
        private readonly IDropDownSubCategoryService subCategoryService;
        private readonly IPracticeService practiceService;
        private readonly ISubPracticeService subPracticeService;
        private readonly IProjectService projectService;

        public ProjectController(IEmployeeService empService,
            IDropDownSubCategoryService subCategoryService,
            IPracticeService practiceService,
            ISubPracticeService subPracticeService, IProjectService projectService)
        {
            this.empService = empService;
            this.subCategoryService = subCategoryService;
            this.practiceService = practiceService;
            this.subPracticeService = subPracticeService;
            this.projectService = projectService;
        }

        // GET: Project
        public ActionResult List()
        {
            IEnumerable<ProjectViewModel> projects = GetProjects();
            return View(projects);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            InitializeAddUpdatePageData();
            return View(new ProjectViewModel());
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProjectViewModel project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (projectService.Exists(project.ProjectName))
                    {
                        ModelState.AddModelError("", "I guess, the Project Name is duplicate");
                        return View(project);
                    }

                    if (projectService.IsDuplicateProjectCode(project.ProjectCode))
                    {
                        ModelState.AddModelError("", "The Project Code is duplicate");
                        return View(project);
                    }

                    ProjectDto projectDto = Mapper.Map<ProjectViewModel, ProjectDto>(project);
                    projectService.Create(projectDto);
                    TempData["AlertMessage"] = "New Project details have been stored successfully";
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
            ProjectViewModel empModel = new ProjectViewModel();
            try
            {
                if (!id.HasValue)
                {
                    ModelState.AddModelError("", "Looks like, the project ID is missing in your request");
                    return View(new ProjectViewModel());
                }

                ProjectDto emp = projectService.GetByID(id.Value);

                if (emp == null)
                {
                    ModelState.AddModelError("", $"Sorry, we couldn't find the Project with ID: {id.Value}");
                    return View(new ProjectViewModel());
                }
                empModel = Mapper.Map<ProjectDto, ProjectViewModel>(emp);
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
        public ActionResult Edit(ProjectViewModel project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (projectService.Exists(project.ProjectName, project.ProjectID))
                    {
                        ModelState.AddModelError("", "I guess, the Project Name is duplicate");
                        return View(project);
                    }

                    if (projectService.IsDuplicateProjectCode(project.ProjectCode, project.ProjectID))
                    {
                        ModelState.AddModelError("", "The Project Code is duplicate");
                        return View(project);
                    }

                    ProjectDto projectDto = Mapper.Map<ProjectViewModel, ProjectDto>(project);
                    projectService.Update(projectDto);
                    TempData["AlertMessage"] = "Project details have been Updated successfully";
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
                ModelState.AddModelError("", "Looks like, the Project ID is missing in your request");
                return View(new ProjectViewModel());
            }

            try
            {
                projectService.Delete(new ProjectDto { ProjectID = id.Value });
                TempData["AlertMessage"] = "Project details have been deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
                return View();
            }
        }

        [HttpPost]
        public JsonResult SubPracticeList(int id)
        {
            IEnumerable<SubPracticeDto> subPracticeList = subPracticeService.GetAllByPracticeID(id);
            List<SelectListItem> ddList = (from c in subPracticeList
                                           orderby c.SubPracticeName
                                           select new SelectListItem
                                           {
                                               Text = c.SubPracticeName,
                                               Value = c.SubPracticeID.ToString()
                                           }).ToList();
            ddList.Insert(0, new SelectListItem
            {
                Text = "Please Select",
                Value = "0",
            });
            return Json(ddList);
        }

        private IEnumerable<ProjectViewModel> GetProjects()
        {
            IEnumerable<ProjectDto> projects = projectService.GetAll();
            IEnumerable<ProjectViewModel> projectModels = Mapper.Map<IEnumerable<ProjectDto>, IEnumerable<ProjectViewModel>>(projects);
            CacheHelper.AddOrUpdateItem(UIConstants.PROJECT_MODELS_LILST, projectModels, HttpContext);
            return projectModels;
        }

        private void InitializeAddUpdatePageData()
        {
            ViewData["IsNewProject"] = true;
            PrepareSubCategoriesDDItems();
            ViewData["PracticeDDList"] = GetPracticeList();
            ViewData["EmployeesDDList"] = GetEmployeesDDList();
            ViewData["SubPracticeDDList"] = new List<SelectListItem>
            {
                new SelectListItem{Text="Please Select", Value="0"}
            };
        }

        private void PrepareSubCategoriesDDItems()
        {
            IEnumerable<DropDownSubCategoryDto> buList = subCategoryService.GetAll();

            List<SelectListItem> projectTypeItems = (from c in buList
                                                     orderby c.SubCategoryName
                                                     where c.CategoryID == (int)CategoryType.ProjectType
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

            ViewData["ProjectTypeDDList"] = projectTypeItems;
        }

        private List<SelectListItem> GetPracticeList()
        {
            List<PracticeDto> practices = practiceService.GetPractices().ToList();
            List<SelectListItem> practiceItems = (from p in practices
                                                  orderby p.PracticeName
                                                  select new SelectListItem
                                                  {
                                                      Text = p.PracticeName,
                                                      Value = p.PracticeID.ToString()
                                                  }).ToList();
            practiceItems.Insert(0, new SelectListItem
            {
                Text = "Please Select",
                Value = "0",
            });

            return practiceItems;
        }

        private List<SelectListItem> GetEmployeesDDList()
        {
            List<EmployeeModel> employeeModels = (List<EmployeeModel>)CacheHelper.GetItem(UIConstants.EMPLOYEE_MODELS_LIST, HttpContext);

            if (employeeModels == null)
            {
                List<EmployeeDto> employees = empService.GetAllEmployees();
                employeeModels = Mapper.Map<List<EmployeeDto>, List<EmployeeModel>>(employees);
                CacheHelper.AddOrUpdateItem(UIConstants.EMPLOYEE_MODELS_LIST, employeeModels, HttpContext);
            }

            List<SelectListItem> empDDList = new List<SelectListItem>
            {
                new SelectListItem{Text = "Please Select", Value = string.Empty}
            };

            if (employeeModels != null)
            {
                foreach (EmployeeModel item in employeeModels)
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
