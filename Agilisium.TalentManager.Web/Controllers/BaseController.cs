using System;
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

        public BaseController()
        {
            CacheHelper.SetContext(HttpContext);
        }

        public virtual void DisplayWarningMessage(string message)
        {
            TempData["WarningMessage"] = message;
        }

        public virtual void DisplaySuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        public virtual void DisplayErrorMessage(string message, Exception exp = null)
        {
            TempData["ErrorMessage"] = message;

            // TODO
            // Error logging
        }

        public virtual void DisplayReadErrorMessage(Exception exp)
        {
            DisplayErrorMessage(readErrorMessage);

            // TODO
            // Error logging
        }

        public virtual void DisplayUpdateErrorMessage(Exception exp)
        {
            DisplayErrorMessage(updateErrorMessage);

            // TODO
            // Error logging
        }

        public virtual void DisplayDeleteErrorMessage(Exception exp)
        {
            DisplayErrorMessage(deleteErrorMessage);

            // TODO
            // Error logging
        }

        public virtual void DisplayLoadErrorMessage(Exception exp)
        {
            DisplayErrorMessage(loadErrorMessage);

            // TODO
            // Error logging
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
    }
}