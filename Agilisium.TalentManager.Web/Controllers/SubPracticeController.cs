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
    public class SubPracticeController : Controller
    {
        private readonly ISubPracticeService subPracticeService;
        private readonly IPracticeService practiceService;

        public SubPracticeController(IPracticeService practiceService, ISubPracticeService subPracticeService)
        {
            this.practiceService = practiceService;
            this.subPracticeService = subPracticeService;
        }

        // GET: SubPractice/1
        public ActionResult Index(string selectedPracticeID)
        {
            SubPracticeListViewModel model = new SubPracticeListViewModel
            {
                Practices = GetPracticesList()
            };

            if (string.IsNullOrEmpty(selectedPracticeID))
            {
                model.SelectedPracticeID = int.Parse(model.Practices.First().Value);
            }
            else
            {
                model.SelectedPracticeID = int.Parse(model.Practices.First(c => c.Value == selectedPracticeID.ToString())?.Value);
            }

            Session["SelectedPracticeID"] = model.SelectedPracticeID.ToString();
            model.SubPractices = GetSubPractices(model.SelectedPracticeID).ToList();
            return View(model);
        }

        // GET: SubPractice/Create
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> listItems = GetPracticesList();
            ViewData["PracticesList"] = listItems;
            return View(new SubPracticeViewModel());
        }

        // POST: SubPractice/Create
        [HttpPost]
        public ActionResult Create(SubPracticeViewModel subPractice)
        {
            try
            {
                IEnumerable<SelectListItem> listItems = GetPracticesList();
                ViewData["PracticesList"] = listItems;

                if (ModelState.IsValid)
                {
                    if (subPracticeService.Exists(subPractice.SubPracticeName))
                    {
                        ModelState.AddModelError("", "This Sub-Practice Name is duplicate");
                        return View(subPractice);
                    }
                    SubPracticeDto subPracticeModel = Mapper.Map<SubPracticeViewModel, SubPracticeDto>(subPractice);
                    subPracticeService.CreateSubPractice(subPracticeModel);
                    TempData["AlertMessage"] = "New Sub-Practice has been stored successfully";
                    Session["SelectedPracticeID"] = subPractice.PracticeID.ToString();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(subPractice);
        }

        // GET: SubPractice/Edit/5
        public ActionResult Edit(int id)
        {
            SubPracticeDto practice = subPracticeService.GetSubPractice(id);
            SubPracticeViewModel uiPractice = Mapper.Map<SubPracticeDto, SubPracticeViewModel>(practice);
            IEnumerable<SelectListItem> listItems = GetPracticesList();
            ViewData["PracticesList"] = listItems;
            return View(uiPractice);
        }

        // POST: SubPractice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SubPracticeViewModel subPracticeModel)
        {
            try
            {
                IEnumerable<SelectListItem> listItems = GetPracticesList();
                ViewData["PracticesList"] = listItems;

                if (ModelState.IsValid || (!ModelState.IsValid && ModelState.Values.Count(p => p.Errors.Count > 0) == 1))
                {
                    if (subPracticeService.Exists(subPracticeModel.SubPracticeName, subPracticeModel.SubPracticeID))
                    {
                        ModelState.AddModelError("", "This Sub-Practice Name is duplicate");
                        return View(subPracticeModel);
                    }

                    SubPracticeDto subPractice = Mapper.Map<SubPracticeViewModel, SubPracticeDto>(subPracticeModel);
                    subPracticeService.UpdateSubPractice(subPractice);
                    TempData["AlertMessage"] = "Sub-Practice has been updated successfully";
                    Session["SelectedPracticeID"] = subPracticeModel.PracticeID.ToString();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(subPracticeModel);
        }

        // GET: SubPractice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                subPracticeService.DeleteSubPractice(new SubPracticeDto { SubPracticeID = id });
                TempData["AlertMessage"] = "Sub-Practice has been deleted successfully";
                int practiceID = int.Parse(Session["SelectedPracticeID"].ToString());
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Oops! an error has occured while deleting");
            }
            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> GetPracticesList()
        {
            var practiceModels = (IEnumerable<PracticeViewModel>)CacheHelper.GetItem(UIConstants.PRACTICE_MODELS_LIST, HttpContext);
            if (practiceModels == null)
            {
                IEnumerable<PracticeDto> practices = practiceService.GetPractices();
                practiceModels = Mapper.Map<IEnumerable<PracticeDto>, IEnumerable<PracticeViewModel>>(practices);
                CacheHelper.AddOrUpdateItem(UIConstants.PRACTICE_MODELS_LIST, practiceModels, HttpContext);
            }

            List<SelectListItem> practicesList = (from cat in practiceModels
                                                  orderby cat.PracticeName
                                                  select new SelectListItem
                                                  {
                                                      Text = cat.PracticeName,
                                                      Value = $"{cat.PracticeID}"
                                                  }).ToList();
            if (practicesList != null && practicesList.Count > 0)
            {
                return practicesList;
            }

            return new List<SelectListItem>
            {
                new SelectListItem{Text = "None", Value = "0"}
            };
        }

        private IEnumerable<SubPracticeViewModel> GetSubPractices(int practiceID)
        {
            IEnumerable<SubPracticeDto> subPractices = subPracticeService.GetSubPractices(practiceID);
            IEnumerable<SubPracticeViewModel> uiPractices = Mapper.Map<IEnumerable<SubPracticeDto>, IEnumerable<SubPracticeViewModel>>(subPractices);
            return uiPractices;
        }
    }
}
