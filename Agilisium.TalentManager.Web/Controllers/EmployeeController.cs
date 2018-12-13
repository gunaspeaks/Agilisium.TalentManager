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
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService empService;
        private readonly IDropDownSubCategoryService subCategoryService;
        private readonly IPracticeService practiceService;
        private readonly ISubPracticeService subPracticeService;

        public EmployeeController(IEmployeeService empService,
            IDropDownSubCategoryService subCategoryService,
            IPracticeService practiceService,
            ISubPracticeService subPracticeService)
        {
            this.empService = empService;
            this.subCategoryService = subCategoryService;
            this.practiceService = practiceService;
            this.subPracticeService = subPracticeService;
        }

        // GET: Employee
        public ActionResult Index()
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            try
            {
                employees = GetEmployees();
            }
            catch (Exception exp)
            {
                SendSuccessMessage(exp.Message);
            }

            return View(employees);
        }

        // GET: Employe/Create
        public ActionResult Create()
        {
            EmployeeViewModel emp = new EmployeeViewModel();

            try
            {
                InitializeAddUpdatePageData(-1);
                emp = new EmployeeViewModel();
            }
            catch (Exception exp)
            {
                SendErrorMessage(exp.Message);
            }

            return View(emp);
        }

        // POST: Employe/Create
        [HttpPost]
        public ActionResult Create(EmployeeViewModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (empService.IsDuplicateEmployeeID(employee.EmployeeID))
                    {
                        SendWarningMessage("I guess, the Employee ID is duplicate");
                        return View(employee);
                    }

                    if (empService.IsDuplicateName(employee.FirstName, employee.LastName))
                    {
                        SendWarningMessage("There is already an Employee with the same First and Last Name");
                        return View(employee);
                    }

                    EmployeeDto employeeDto = Mapper.Map<EmployeeViewModel, EmployeeDto>(employee);
                    empService.Create(employeeDto);
                    SendSuccessMessage("New Employee details have been stored successfully");
                }
            }
            catch (Exception exp)
            {
                SendWarningMessage(exp.Message);
            }
            return RedirectToAction("Index");
        }

        // GET: Employe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                SendWarningMessage("Looks like, the employee ID is missing in your request");
                return View(new EmployeeViewModel());
            }

            EmployeeViewModel empModel = new EmployeeViewModel();
            try
            {
                EmployeeDto emp = empService.GetEmployee(id.Value);

                if (emp == null)
                {
                    SendWarningMessage($"Sorry, We couldn't find the Employee with ID: {id.Value}");
                    return View(new EmployeeViewModel());
                }
                empModel = Mapper.Map<EmployeeDto, EmployeeViewModel>(emp);
                InitializeAddUpdatePageData(empModel.PracticeID);
            }
            catch (Exception exp)
            {
                SendErrorMessage(exp.Message);
            }
            return View(empModel);
        }

        // POST: Employe/Edit/5
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (empService.IsDuplicateEmployeeID(employee.EmployeeEntryID, employee.EmployeeID))
                    {
                        ModelState.AddModelError("", "I guess, the Employee ID is duplicate");
                        return View(employee);
                    }

                    if (empService.IsDuplicateName(employee.EmployeeEntryID, employee.FirstName, employee.LastName))
                    {
                        ModelState.AddModelError("", "There is already an Employee with the same First and Last Name");
                        return View(employee);
                    }

                    EmployeeDto employeeDto = Mapper.Map<EmployeeViewModel, EmployeeDto>(employee);
                    empService.Update(employeeDto);
                    TempData["AlertMessage"] = "Employee details have been Updated successfully";
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return RedirectToAction("Index");
        }

        // GET: Employe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                ModelState.AddModelError("", "Looks like, the employee ID is missing in your request");
                return View(new EmployeeViewModel());
            }

            try
            {
                empService.Delete(new EmployeeDto { EmployeeEntryID = id.Value });
                TempData["AlertMessage"] = "Employee details have been deleted successfully";
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
            return Json(GetSubPractices(id));
        }

        private List<EmployeeViewModel> GetEmployees()
        {
            List<EmployeeDto> employees = empService.GetAllEmployees().ToList();
            List<EmployeeViewModel> employeeModels = Mapper.Map<List<EmployeeDto>, List<EmployeeViewModel>>(employees);
            CacheHelper.AddOrUpdateItem(UIConstants.EMPLOYEE_MODELS_LIST, employeeModels, HttpContext);

            return employeeModels;
        }

        private void InitializeAddUpdatePageData(int subPracticeID, int employeeID = -1)
        {
            ViewBag.IsNewEntry = true;
            GetPracticeList();
            GetProjectManagersList(employeeID);
            GetSubPractices(subPracticeID);
            GetOtherDropDownItems();
        }

        private void GetOtherDropDownItems()
        {
            List<DropDownSubCategoryDto> buList = subCategoryService.GetSubCategories();

            List<SelectListItem> buListItems = (from c in buList
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.BusinessUnit
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();

            InsertDefaultListItem(buListItems);
            ViewBag.BUsList = buListItems;

            List<SelectListItem> ucListItems = (from c in buList
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.UtilizationCode
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();

            InsertDefaultListItem(ucListItems);
            ViewBag.UtilizationTypesList = ucListItems;

            List<SelectListItem> empTypeList = (from c in buList
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.EmploymentType
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();

            InsertDefaultListItem(empTypeList);
            ViewBag.EmploymentTypesList = empTypeList;
        }

        private void GetPracticeList()
        {
            List<PracticeDto> practices = practiceService.GetPractices().ToList();
            List<SelectListItem> practiceItems = (from p in practices
                                                  orderby p.PracticeName
                                                  select new SelectListItem
                                                  {
                                                      Text = p.PracticeName,
                                                      Value = p.PracticeID.ToString()
                                                  }).ToList();
            InsertDefaultListItem(practiceItems);
            ViewBag.PracticeList = practiceItems;
        }

        private List<SelectListItem> GetSubPractices(int practiceID)
        {
            List<SubPracticeDto> subPracticeList = subPracticeService.GetSubPractices(practiceID);
            List<SelectListItem> ddList = (from c in subPracticeList
                                           orderby c.SubPracticeName
                                           select new SelectListItem
                                           {
                                               Text = c.SubPracticeName,
                                               Value = c.SubPracticeID.ToString()
                                           }).ToList();

            InsertDefaultListItem(ddList);
            ViewBag.SubPracticesList = ddList;
            return ddList;
        }

        private void GetProjectManagersList(int employeeID)
        {
            List<EmployeeDto> managers = empService.GetAllManagers();

            List<SelectListItem> empDDList = new List<SelectListItem>();
            InsertDefaultListItem(empDDList);

            if (managers != null)
            {
                foreach (EmployeeDto item in managers)
                {
                    empDDList.Add(new SelectListItem
                    {
                        Text = $"{item.LastName}, {item.FirstName}",
                        Value = item.EmployeeEntryID.ToString()
                    });
                }
            }

            ViewBag.ProjectManagersList = empDDList;
        }
    }
}
