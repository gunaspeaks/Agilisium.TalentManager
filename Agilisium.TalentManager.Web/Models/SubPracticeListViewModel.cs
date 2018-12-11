using System.Collections.Generic;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Models
{
    public class SubPracticeListViewModel : ViewModelBase
    {
        public SubPracticeListViewModel()
        {
            Practices = new List<SelectListItem>();
            SubPractices = new List<SubPracticeViewModel>();
        }

        public IEnumerable<SelectListItem> Practices { get; set; }

        public int SelectedPracticeID { get; set; }

        public IEnumerable<SubPracticeViewModel> SubPractices { get; set; }
    }
}