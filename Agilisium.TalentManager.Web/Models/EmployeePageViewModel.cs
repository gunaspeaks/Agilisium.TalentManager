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
        }

        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}