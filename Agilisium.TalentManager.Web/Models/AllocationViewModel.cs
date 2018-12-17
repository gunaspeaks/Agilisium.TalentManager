using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class AllocationViewModel : ViewModelBase
    {
        public IEnumerable<AllocationModel> Allocations { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public AllocationViewModel()
        {
            Allocations = new List<AllocationModel>();
        }
    }
}