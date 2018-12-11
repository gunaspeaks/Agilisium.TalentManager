using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Models
{
    public class EmployeePageViewModel : ViewModelBase
    {
        public EmployeePageViewModel()
        {
            Employees = new List<EmployeeViewModel>();
            BusinessUnits = new List<SelectListItem>();
            Practices = new List<SelectListItem>();
            SubPractices = new List<SelectListItem>();
        }

        public List<EmployeeViewModel> Employees { get; set; }

        public List<SelectListItem> BusinessUnits { get; set; }

        public List<SelectListItem> Practices { get; set; }

        public List<SelectListItem> SubPractices { get; set; }
    }
}