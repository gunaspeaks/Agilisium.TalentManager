namespace Agilisium.TalentManager.Dto
{
    public class EmployeePodAllocationDto : DtoBase
    {
        public int AllocationEntryID { get; set; }

        public int EmployeeEntryID { get; set; }

        public int PodID { get; set; }

        public string PodName { get; set; }

        public int PercentageOfAllocation { get; set; }
    }
}
