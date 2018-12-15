﻿using Agilisium.TalentManager.Dto;
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
    public class SubPracticeController : BaseController
    {
        private readonly ISubPracticeService subPracticeService;
        private readonly IPracticeService practiceService;

        public SubPracticeController(IPracticeService practiceService, ISubPracticeService subPracticeService)
        {
            this.practiceService = practiceService;
            this.subPracticeService = subPracticeService;
        }

        // GET: SubPractice/1
        public ActionResult List(string selectedPracticeID, int page = 1)
        {
            SubPracticeViewModel model = new SubPracticeViewModel();

            try
            {
                model.PracticeListItems = (IEnumerable<SelectListItem>)Session["PracticeListItems"] ?? GetPracticesDropDownList();

                if (string.IsNullOrEmpty(selectedPracticeID))
                {
                    if (Session["SelectedPracticeID"] == null
                        || (Session["SelectedPracticeID"] != null && string.IsNullOrEmpty(Session["SelectedPracticeID"].ToString())))
                    {
                        model.SelectedPracticeID = int.Parse(model.PracticeListItems.FirstOrDefault(c => c.Text != "Please Select")?.Value);
                    }
                    else
                    {
                        model.SelectedPracticeID = int.Parse(Session["SelectedPracticeID"].ToString());
                    }
                }
                else
                {
                    model.SelectedPracticeID = int.Parse(selectedPracticeID);
                }

                model.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = subPracticeService.TotalRecordsCountByPracticeID(model.SelectedPracticeID),
                    CurentPageNo = page,
                    RecordsPerPage = RecordsPerPage
                };

                if (model.PagingInfo.TotalRecordsCount > 0)
                {
                    Session["SelectedPracticeID"] = model.SelectedPracticeID.ToString();
                    model.SubPractices = GetSubPractices(model.SelectedPracticeID, page);
                }
                else
                {
                    string practiceName = practiceService.GetPracticeName(model.SelectedPracticeID);
                    if (string.IsNullOrEmpty(practiceName))
                    {
                        DisplayWarningMessage("Hey, please check whether you are trying to access the correct Practice.");
                    }
                    else
                    {
                        DisplayWarningMessage($"There are no Sub-Practices found for Practice '{practiceName}'");
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }

            return View(model);
        }

        // GET: SubPractice/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.PracticeListItems = (IEnumerable<SelectListItem>)Session["PracticeListItems"] ?? GetPracticesDropDownList();
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(new SubPracticeModel());
        }

        // POST: SubPractice/Create
        [HttpPost]
        public ActionResult Create(SubPracticeModel subPractice)
        {
            try
            {
                ViewBag.PracticeListItems = (IEnumerable<SelectListItem>)Session["PracticeListItems"] ?? GetPracticesDropDownList();

                if (ModelState.IsValid)
                {
                    if (subPracticeService.Exists(subPractice.SubPracticeName))
                    {
                        DisplayWarningMessage($"The Sub-Practice Name '{subPractice.SubPracticeName}' is duplicate");
                        return View(subPractice);
                    }

                    SubPracticeDto subPracticeModel = Mapper.Map<SubPracticeModel, SubPracticeDto>(subPractice);
                    subPracticeService.CreateSubPractice(subPracticeModel);
                    DisplaySuccessMessage($"New Sub-Practice '{subPractice.SubPracticeName}' has been stored successfully");
                    Session["SelectedPracticeID"] = subPractice.PracticeID.ToString();
                    return RedirectToAction("List");
                }

            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(subPractice);
        }

        // GET: SubPractice/Edit/5
        public ActionResult Edit(int? id)
        {
            SubPracticeModel uiPractice = new SubPracticeModel();

            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the ID is missing in your request");
                return RedirectToAction("List");
            }

            try
            {
                if (!subPracticeService.Exists(id.Value))
                {
                    DisplayWarningMessage($"Sorry, We couldn't find the Sub-Practice with ID: {id.Value}");
                    return RedirectToAction("List");
                }

                ViewBag.PracticeListItems = (IEnumerable<SelectListItem>)Session["PracticeListItems"] ?? GetPracticesDropDownList();

                SubPracticeDto practice = subPracticeService.GetByID(id.Value);
                uiPractice = Mapper.Map<SubPracticeDto, SubPracticeModel>(practice);
            }
            catch (Exception exp)
            {
                DisplayReadErrorMessage(exp);
            }

            return View(uiPractice);
        }

        // POST: SubPractice/Edit/5
        [HttpPost]
        public ActionResult Edit(SubPracticeModel subPractice)
        {
            try
            {
                ViewBag.PracticeListItems = (IEnumerable<SelectListItem>)Session["PracticeListItems"] ?? GetPracticesDropDownList();

                if (ModelState.IsValid)
                {
                    if (subPracticeService.Exists(subPractice.SubPracticeName, subPractice.SubPracticeID))
                    {
                        DisplayWarningMessage($"The Sub-Practice Name '{subPractice.SubPracticeName}' is duplicate");
                        return View(subPractice);
                    }

                    SubPracticeDto subPracticeDto = Mapper.Map<SubPracticeModel, SubPracticeDto>(subPractice);
                    subPracticeService.UpdateSubPractice(subPracticeDto);
                    DisplaySuccessMessage("Sub-Practice has been updated successfully");
                    Session["SelectedPracticeID"] = subPractice.PracticeID.ToString();
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                DisplayUpdateErrorMessage(exp);
            }
            return View(subPractice);
        }

        // GET: SubPractice/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                DisplayWarningMessage("Looks like, the ID is missing in your request");
                return RedirectToAction("List");
            }

            try
            {
                if (subPracticeService.CanBeDeleted(id.Value) == false)
                {
                    DisplayWarningMessage("There are some dependencies with this Sub-Practice. So, you can't delete this for now.");
                    return RedirectToAction("List");
                }

                subPracticeService.DeleteSubPractice(new SubPracticeDto { SubPracticeID = id.Value });
                DisplaySuccessMessage("Sub-Practice has been deleted successfully");
                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                DisplayDeleteErrorMessage(exp);
            }
            return RedirectToAction("List");
        }

        private IEnumerable<SelectListItem> GetPracticesDropDownList()
        {
            IEnumerable<PracticeDto> practices = practiceService.GetPractices();
            IEnumerable<PracticeModel> practiceModels = Mapper.Map<IEnumerable<PracticeDto>, IEnumerable<PracticeModel>>(practices);
            List<SelectListItem> practicesList = (from cat in practiceModels
                                                  orderby cat.PracticeName
                                                  select new SelectListItem
                                                  {
                                                      Text = cat.PracticeName,
                                                      Value = $"{cat.PracticeID}"
                                                  }).ToList();

            InsertDefaultListItem(practicesList);
            Session["PracticeListItems"] = practicesList;
            return practicesList;
        }

        private IEnumerable<SubPracticeModel> GetSubPractices(int practiceID, int pageNo)
        {
            IEnumerable<SubPracticeDto> subPractices = subPracticeService.GetAllByPracticeID(practiceID, RecordsPerPage, pageNo);
            IEnumerable<SubPracticeModel> uiPractices = Mapper.Map<IEnumerable<SubPracticeDto>, IEnumerable<SubPracticeModel>>(subPractices);
            return uiPractices;
        }
    }
}
