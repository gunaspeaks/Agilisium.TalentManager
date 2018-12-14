using System.Collections.Generic;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Helpers
{
    public class BaseController : Controller
    {
        private const string readErrorMessage = "Oops! an error has occured while retrieving the details";
        private const string updateErrorMessage = "Oops! an error has occured while updating the details";
        private const string deleteErrorMessage = "Oops! an error has occured while deleting the details";
        private const string loadErrorMessage = "Oops! an error has occured while loading the page";

        public virtual void SendWarningMessage(string message)
        {
            TempData["WarningMessage"] = message;
        }

        public virtual void SendSuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        public virtual void SendErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
        }

        public virtual void SendReadErrorMessage()
        {
            SendErrorMessage(readErrorMessage);
        }

        public virtual void SendUpdateErrorMessage()
        {
            SendErrorMessage(updateErrorMessage);
        }

        public virtual void SendDeleteErrorMessage()
        {
            SendErrorMessage(deleteErrorMessage);
        }

        public virtual void SendLoadErrorMessage()
        {
            SendErrorMessage(loadErrorMessage);
        }

        public bool IsPaginationEnabled
        {
            get
            {
                object configValue = HttpContext.Application[UIConstants.CONFIG_ENABLE_PAGINATION];
                if (configValue == null)
                {
                    return true;
                }

                return configValue.ToString().ToLower() == "true";
            }
        }

        public int RecordsPerPage
        {
            get
            {
                if (IsPaginationEnabled)
                {
                    object pageSizeValue = HttpContext.Application[UIConstants.CONFIG_RECORDS_PER_PAGE];

                    if (int.TryParse(pageSizeValue.ToString(), out int pageSize))
                    {
                        return 10;
                    }
                    return pageSize;
                }
                else
                {
                    return -1;
                }
            }
        }

        public void InsertDefaultListItem(List<SelectListItem> itemsList)
        {
            if (itemsList == null)
            {
                itemsList = new List<SelectListItem>();
            }

            itemsList.Insert(0, new SelectListItem
            {
                Text = "Please Select",
                Value = "0",
            });
        }
    }
}