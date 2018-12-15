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
    public class PracticeController : BaseController
    {
        private readonly IPracticeService service;

        public PracticeController(IPracticeService practiceService)
        {
            service = practiceService;
        }

        // GET: Practice
        public ActionResult List(int page = 1)
        {
            PracticeViewModel viewModel = new PracticeViewModel();

            try
            {
                viewModel.PagingInfo = new PagingInfo
                {
                    TotalRecordsCount = service.TotalRecordsCount(),
                    RecordsPerPage = RecordsPerPage,
                    CurentPageNo = page
                };

                if (viewModel.PagingInfo.TotalRecordsCount > 0)
                {
                    viewModel.Practices = GetPractices(page);
                }
                else
                {
                    SendWarningMessage("There are no Practices to display");
                }
            }
            catch (Exception exp)
            {
                SendLoadErrorMessage(exp);
            }

            return View(viewModel);
        }

        // GET: Practice/Create
        public ActionResult Create()
        {
            return View(new PracticeModel());
        }

        // POST: Practice/Create
        [HttpPost]
        public ActionResult Create(PracticeModel practice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (service.Exists(practice.PracticeName))
                    {
                        SendWarningMessage($"The Practice Name '{practice.PracticeName}' is duplicate");
                        return View(practice);
                    }
                    PracticeDto practiceModel = Mapper.Map<PracticeModel, PracticeDto>(practice);
                    service.CreatePractice(practiceModel);
                    SendSuccessMessage($"New Practice '{practice.PracticeName}' has been stored successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                SendLoadErrorMessage(exp);
            }
            return View(practice);
        }

        // GET: Practice/Edit/5
        public ActionResult Edit(int? id)
        {
            PracticeModel practice = new PracticeModel();

            if (!id.HasValue)
            {
                SendWarningMessage("Looks like, the ID is missing in your request");
                return RedirectToAction("List");
            }

            try
            {
                if (!service.Exists(id.Value))
                {
                    SendWarningMessage($"Sorry, We couldn't find the Practice with ID: {id.Value}");
                    return RedirectToAction("List");
                }

                PracticeDto practiceDto = service.GetPractice(id.Value);
                practice = Mapper.Map<PracticeDto, PracticeModel>(practiceDto);
            }
            catch (Exception exp)
            {
                SendReadErrorMessage(exp);
            }

            return View(practice);
        }

        // POST: Practice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PracticeModel practice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (service.Exists(practice.PracticeName, practice.PracticeID))
                    {
                        SendWarningMessage($"Practice Name '{practice.PracticeName}' is duplicate");
                        return View(practice);
                    }

                    PracticeDto practiceModel = Mapper.Map<PracticeModel, PracticeDto>(practice);
                    service.UpdatePractice(practiceModel);
                    SendSuccessMessage($"Practice '{practice.PracticeName}' details have been modified successfully");
                    return RedirectToAction("List");
                }
            }
            catch (Exception exp)
            {
                SendUpdateErrorMessage(exp);
            }
            return View(practice);
        }

        // POST: Practice/Delete/5
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
                if (service.IsReservedEntry(id.Value))
                {
                    SendWarningMessage("Hey, why do you want to delete a Reserved Practice. Please check with the system administrator.");
                    return RedirectToAction("List");
                }

                if (service.CanBeDeleted(id.Value) == false)
                {
                    SendWarningMessage("There are some dependencies with this Practice. So, you can't delete this for now");
                    return RedirectToAction("List");
                }

                service.DeletePractice(new PracticeDto { PracticeID = id.Value });
                SendSuccessMessage("Practice has been deleted successfully");
            }
            catch(Exception exp)
            {
                SendDeleteErrorMessage(exp);
            }
            return RedirectToAction("List");
        }

        private IEnumerable<PracticeModel> GetPractices(int pageNo)
        {
            IEnumerable<PracticeDto> practices = service.GetPractices(RecordsPerPage, pageNo);
            IEnumerable<PracticeModel> practiceModels = Mapper.Map<IEnumerable<PracticeDto>, IEnumerable<PracticeModel>>(practices);
            return practiceModels;
        }
    }
}
