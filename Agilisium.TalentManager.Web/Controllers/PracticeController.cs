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
    public class PracticeController : Controller
    {
        private readonly IPracticeService service;

        public PracticeController(IPracticeService practiceService)
        {
            service = practiceService;
        }

        // GET: Practice
        public ActionResult Index()
        {
            IEnumerable<PracticeViewModel> practices = GetPractices();
            return View(practices);
        }

        // GET: Practice/Create
        public ActionResult Create()
        {
            return View(new PracticeViewModel());
        }

        // POST: Practice/Create
        [HttpPost]
        public ActionResult Create(PracticeViewModel practice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (service.Exists(practice.PracticeName))
                    {
                        ModelState.AddModelError("", "This Practice Name is duplicate");
                        return View(practice);
                    }
                    PracticeDto practiceModel = Mapper.Map<PracticeViewModel, PracticeDto>(practice);
                    service.CreatePractice(practiceModel);
                    TempData["AlertMessage"] = "New Practice has been stored successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(practice);
        }

        // GET: Practice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return View("Please select a Practice to Edit");
            }
            if (!service.Exists(id.Value))
            {
                return View($"Sorry, We couldn't find the Practice with ID: {id.Value}");
            }
            PracticeDto practice = service.GetPractice(id.Value);
            PracticeViewModel uiPractice = Mapper.Map<PracticeDto, PracticeViewModel>(practice);
            return View(uiPractice);
        }

        // POST: Practice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PracticeViewModel practice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (service.Exists(practice.PracticeName, practice.PracticeID))
                    {
                        ModelState.AddModelError("", "This Practice Name is duplicate");
                        return View(practice);
                    }
                    PracticeDto practiceModel = Mapper.Map<PracticeViewModel, PracticeDto>(practice);
                    service.UpdatePractice(practiceModel);
                    TempData["AlertMessage"] = "Practice has been updated successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(practice);
        }

        // POST: Practice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.DeletePractice(id);
                TempData["AlertMessage"] = "Practice has been deleted successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        private IEnumerable<PracticeViewModel> GetPractices()
        {
            IEnumerable<PracticeDto> practices = service.GetPractices()?.OrderBy(p => p.PracticeName);
            IEnumerable<PracticeViewModel> practiceModels = Mapper.Map<IEnumerable<PracticeDto>, IEnumerable<PracticeViewModel>>(practices);
            CacheHelper.AddOrUpdateItem(UIConstants.PRACTICE_MODELS_LIST, practiceModels, HttpContext);
            return practiceModels;
        }
    }
}
