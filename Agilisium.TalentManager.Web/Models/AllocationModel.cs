﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class AllocationModel : ViewModelBase
    {
        public int AllocationEntryID { get; set; }

        [Required(ErrorMessage = "Please select an Employee")]
        [DisplayName("Employee Name")]
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Allocation Start Date is required")]
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AllocationStartDate { get; set; }

        [Required(ErrorMessage = "Allocation End Date is required")]
        [DisplayName("End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AllocationEndDate { get; set; }

        [Required(ErrorMessage = "Please select a Allocation Type")]
        [DisplayName("Allocation Type")]
        public int AllocationTypeID { get; set; }

        [DisplayName("Allocation Type")]
        public string AllocationTypeName { get; set; }

        [Required(ErrorMessage = "Please select a Project")]
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