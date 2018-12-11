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
    public class EmployeeController : Controller
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
            List<EmployeeViewModel> employees = GetEmployees();
            return View(employees);
        }

        // GET: Employe/Create
        public ActionResult Create()
        {
            InitializeAddUpdatePageData(-1);
            return View(new EmployeeViewModel());
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
                        ModelState.AddModelError("", "I guess, the Employee ID is duplicate");
                        return View(employee);
                    }

                    if (empService.IsDuplicateName(employee.FirstName, employee.LastName))
                    {
                        ModelState.AddModelError("", "There is already an Employee with the same First and Last Name");
                        return View(employee);
                    }

                    EmployeeDto employeeDto = Mapper.Map<EmployeeViewModel, EmployeeDto>(employee);
                    empService.Create(employeeDto);
                    TempData["AlertMessage"] = "New Employee details have been stored successfully";
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return RedirectToAction("Index");
        }

        // GET: Employe/Edit/5
        public ActionResult Edit(int? id)
        {
            EmployeeViewModel empModel = new EmployeeViewModel();
            try
            {
                if (!id.HasValue)
                {
                    ModelState.AddModelError("", "Looks like, the employee ID is missing in your request");
                    return View(new EmployeeViewModel());
                }

                var emp = empService.GetEmployee(id.Value);

                if (emp == null)
                {
                    ModelState.AddModelError("", $"Sorry, We couldn't find the Employee with ID: {id.Value}");
                    return View(new EmployeeViewModel());
                }
                empModel = Mapper.Map<EmployeeDto, EmployeeViewModel>(emp);
                InitializeAddUpdatePageData(empModel.PracticeID);
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
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
                empService.Delete(id.Value);
                TempData["AlertMessage"] = "Employee details have been deleted successfully";
                return RedirectToAction("Index");
            }
            catch(Exception exp)
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

        private void InitializeAddUpdatePageData(int subPracticeID)
        {
            ViewData["IsNewEntry"] = true;
            PrepareSubCategoriesDDItems();
            ViewData["PracticeDDList"] = GetPracticeList();
            ViewData["EmployeesDDList"] = GetEmployeesDDList();
            ViewData["SubPracticeDDList"] = GetSubPractices(subPracticeID);
        }

        private void PrepareSubCategoriesDDItems()
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

            buListItems.Insert(0, new SelectListItem
            {
                Text = "Please Select",
                Value = "0",
            });

            ViewData["BuDDList"] = buListItems;

            List<SelectListItem> ucListItems = (from c in buList
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.UtilizationCode
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();
            ucListItems.Insert(0, new SelectListItem
            {
                Text = "Please Select",
                Value = "0",
            });

            ViewData["UtilizationTypeDDList"] = ucListItems;
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
            ddList.Insert(0, new SelectListItem
            {
                Text = "Please Select",
                Value = "0",
            });
            return ddList;
        }

        private List<SelectListItem> GetEmployeesDDList()
        {
            List<EmployeeViewModel> employeeModels = (List<EmployeeViewModel>)CacheHelper.GetItem(UIConstants.EMPLOYEE_MODELS_LIST, HttpContext);
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
