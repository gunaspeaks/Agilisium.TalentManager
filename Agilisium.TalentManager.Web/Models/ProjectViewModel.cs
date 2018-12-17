using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class ProjectViewModel : ViewModelBase
    {
        public IEnumerable<ProjectModel> Projects { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public ProjectViewModel()
        {
            Projects = new List<ProjectModel>();
        }
    }
}