using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class AllocationViewModel : ViewModelBase
    {
        [DisplayName("")]
        public int AllocationEntryID { get; set; }

        [DisplayName("Employee Name")]
        public int EmployeeID { get; set; }

        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AllocationStartDate { get; set; }

        [DisplayName("End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AllocationEndDate { get; set; }

        [DisplayName("Allocation Type")]
        public int AllocationTypeID { get; set; }

        [DisplayName("Allocation Type")]
        public string AllocationTypeName { get; set; }

        [DisplayName("Project Name")]
        public int ProjectID { get; set; }

        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [DisplayName("Allocation %")]
        public int PercentageOfAllocation { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
    }
}