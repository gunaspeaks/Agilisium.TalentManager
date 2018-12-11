using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class AllocationsModel
    {
        public List<AllocationModel> TopAllocations { get; set; }
    }

    public class AllocationModel
    {
        public string ProjectName { get; set; }

        public int HeadCount { get; set; }
    }
}