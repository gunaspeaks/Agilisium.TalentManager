namespace Agilisium.TalentManager.Model.Entities
{
    public class EmployeePodAllocation : EntityBase
    {
        public int AllocationEntryID { get; set; }

        public int EmployeeEntryID { get; set; }

        public int PodID { get; set; }

        public int PercentageOfAllocation { get; set; }
    }
}
