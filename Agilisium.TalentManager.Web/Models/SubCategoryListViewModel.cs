using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Models
{
    public class SubCategoryListViewModel : ViewModelBase
    {
        public SubCategoryListViewModel()
        {
            Categories = new List<SelectListItem>();
            SubCategories = new List<SubCategoryViewModel>();
        }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public int SelectedCategoryID { get; set; }

        public IEnumerable<SubCategoryViewModel> SubCategories { get; set; }
    }
}