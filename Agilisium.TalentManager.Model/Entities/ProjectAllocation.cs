using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Model.Entities
{
    public class ProjectAllocation : EntityBase
    {
        public int AllocationEntryID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime? AllocationStartDate { get; set; }

        public DateTime? AllocationEndDate { get; set; }

        public int AllocationTypeID { get; set; }

        public int ProjectID { get; set; }

        public string Remarks { get; set; }

        public int PercentageOfAllocation { get; set; }
    }
}
