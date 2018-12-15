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
        public ActionResult List(int page = 1)
        {
            EmployeeViewModel viewModel = new EmployeeViewModel();

            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = empService.TotalRecordsCount(),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.Employees = GetEmployees(page);
                }
                else
                {
                    DisplayWarningMessage("There are no Employees to display");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        // GET: Employe/Create
        public ActionResult Create()
        {
            EmployeeModel emp = new EmployeeModel()
            {
                DateOfJoin = DateTime.Now
            };

            try
            {
                InitializePageData(-1);
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(emp);
        }

        // POST: Employe/Create
        [HttpPost]
        public ActionResult Create(EmployeeModel employee)
        {
            InitializePageData(-1);

            try
            {
                if (ModelState.IsValid)
                {
                    if (empService.IsDuplicateName(employee.FirstName, employee.LastName))
                    {
                        DisplayWarningMessage("There is already an Employee with the same First and Last Name");
                        return View(employee);
                    }

                    EmployeeDto employeeDto = Mapper.Map<EmployeeModel, EmployeeDto>(employee);
                    empService.Create(employeeDto);
                    DisplaySuccessMessage("New Employee details have been stored successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayWarningMessage(exp.Message);
            }
            return View(employee);
        }

        // GET: Employe/Edit/5
        public ActionResult Edit(int? id)
        {
            EmployeeModel empModel = new EmployeeModel();
            InitializePageData(empModel.PracticeID, id.HasValue ? id.Value : -1);

            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the employee ID is missing in your request");
                return View(empModel);
            }

            try
            {

                if (!empService.Exists(id.Value))
                {
                    DisplayWarningMessage($"Sorry, we couldn't find the Employee with ID: {id.Value}");
                    return View(empModel);
                }

                EmployeeDto emp = empService.GetEmployee(id.Value);
                empModel = Mapper.Map<EmployeeDto, EmployeeModel>(emp);
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }
            return View(empModel);
        }

        // POST: Employe/Edit/5
        [HttpPost]
        public ActionResult Edit(EmployeeModel employee)
        {
            try
            {
                InitializePageData(employee.PracticeID, employee.EmployeeEntryID);
                if (ModelState.IsValid)
                {
                    if (empService.IsDuplicateName(employee.EmployeeEntryID, employee.FirstName, employee.LastName))
                    {
                        DisplayWarningMessage("There is already an Employee with the same First and Last Name");
                        return View(employee);
                    }

                    EmployeeDto employeeDto = Mapper.Map<EmployeeModel, EmployeeDto>(employee);
                    empService.Update(employeeDto);
                    DisplaySuccessMessage("Employee details have been Updated successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(employee);
        }

        // GET: Employe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                ModelState.AddModelError("", "Looks like, the employee ID is missing in your request");
                return RedirectToAction("List");
            }

            try
            {
                empService.Delete(new EmployeeDto { EmployeeEntryID = id.Value });
                DisplaySuccessMessage("Employee details have been deleted successfully");
                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                DisplayDeleteErrorMessage(exp);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public JsonResult GetSubPracticeList(int id)
        {
            return Json(GetSubPractices(id));
        }

        [HttpPost]
        public string GenerateNewEmployeeID(int id)
        {
            return empService.GenerateNewEmployeeID(id);
        }

        private IEnumerable<EmployeeModel> GetEmployees(int pageNo)
        {
            IEnumerable<EmployeeDto> employees = empService.GetAllEmployees(RecordsPerPage, pageNo);
            IEnumerable<EmployeeModel> employeeModels = Mapper.Map<IEnumerable<EmployeeDto>, IEnumerable<EmployeeModel>>(employees);

            return employeeModels;
        }

        private void InitializePageData(int subPracticeID = -1, int employeeID = -1)
        {
            ViewBag.IsNewEntry = true;
            GetPracticeList();
            GetReportingManagersList(employeeID);
            GetSubPractices(subPracticeID);
            GetOtherDropDownItems();
        }

        private void GetOtherDropDownItems()
        {
            IEnumerable<DropDownSubCategoryDto> buList = subCategoryService.GetAll();

            List<SelectListItem> buListItems = (from c in buList
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.BusinessUnit
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();

            InsertDefaultListItem(buListItems);
            ViewBag.BuListItems = buListItems;

            List<SelectListItem> ucListItems = (from c in buList
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.UtilizationCode
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();

            InsertDefaultListItem(ucListItems);
            ViewBag.UtilizationTypeListItems = ucListItems;

            List<SelectListItem> empTypeList = (from c in buList
                                                orderby c.SubCategoryName
                                                where c.CategoryID == (int)CategoryType.EmploymentType
                                                select new SelectListItem
                                                {
                                                    Text = c.SubCategoryName,
                                                    Value = c.SubCategoryID.ToString()
                                                }).ToList();

            InsertDefaultListItem(empTypeList);
            ViewBag.EmploymentTypeListItems = empTypeList;
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
            ViewBag.PracticeListItems = practiceItems;
        }

        private List<SelectListItem> GetSubPractices(int practiceID)
        {
            IEnumerable<SubPracticeDto> subPracticeList = subPracticeService.GetAllByPracticeID(practiceID);
            List<SelectListItem> ddList = (from c in subPracticeList
                                           orderby c.SubPracticeName
                                           select new SelectListItem
                                           {
                                               Text = c.SubPracticeName,
                                               Value = c.SubPracticeID.ToString()
                                           }).ToList();

            InsertDefaultListItem(ddList);
            ViewBag.SubPracticeListItems = ddList;
            return ddList;
        }

        private void GetReportingManagersList(int employeeID)
        {
            List<EmployeeDto> managers = empService.GetAllManagers();

            List<SelectListItem> empDDList = new List<SelectListItem>();

            if (managers != null)
            {
                empDDList = (from e in managers
                             where e.EmployeeEntryID != employeeID
                             select new SelectListItem
                             {
                                 Text = $"{e.LastName}, {e.FirstName}",
                                 Value = e.EmployeeEntryID.ToString()
                             }).ToList();
            }

            InsertDefaultListItem(empDDList);

            ViewBag.ReportingManagerListItems = empDDList;
        }
    }
}
