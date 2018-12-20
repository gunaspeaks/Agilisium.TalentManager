using System;

namespace Agilisium.TalentManager.Dto
{
    public class CustomAllocationDto : DtoBase
    {
        public string ProjectName { get; set; }

        public string ProjectCode { get; set; }

        public string ProjectManager { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int AllocatedPercentage { get; set; }

        public string UtilizatinType { get; set; }
    }
}
