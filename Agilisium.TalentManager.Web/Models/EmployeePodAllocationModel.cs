using System.ComponentModel;

namespace Agilisium.TalentManager.Web.Models
{
    public class EmployeePodAllocationModel : ViewModelBase
    {
        public int AllocationEntryID { get; set; }

        public int EmployeeEntryID { get; set; }

        [DisplayName("POD Name")]
        public int PodID { get; set; }

        [DisplayName("POD Name")]
        public string PodName { get; set; }

        [DisplayName("Allocation %")]
        public int PercentageOfAllocation { get; set; }
    }
}