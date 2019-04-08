using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Web.Models
{
    public class EmployeeModel : ViewModelBase
    {
        public EmployeeModel()
        {
            PodAllocations = new List<EmployeePodAllocationModel>();
        }

        public int EmployeeEntryID { get; set; }

        [DisplayName("Employee ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee ID is required")]
        [MaxLength(10, ErrorMessage = "Employee ID should not exceed 10 characters")]
        public string EmployeeID { get; set; }

        [DisplayName("First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
        [MaxLength(100, ErrorMessage = "First Name should not exceed 100 characters")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
        [MaxLength(100, ErrorMessage = "Last Name should not exceed 100 characters")]
        public string LastName { get; set; }

        [DisplayName("Email ID")]
        [MaxLength(100, ErrorMessage = "Last Name should not exceed 100 characters")]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        [DisplayName("Business Unit")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a Business Unit")]
        public int BusinessUnitID { get; set; }

        [DisplayName("Business Unit")]
        public string BusinessUnitName { get; set; }

        [DisplayName("POD")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a Practice")]
        public int PracticeID { get; set; }

        [DisplayName("POD")]
        public string PracticeName { get; set; }

        [DisplayName("Competency")]
        public int? SubPracticeID { get; set; }

        [DisplayName("Competency")]
        public string SubPracticeName { get; set; }

        [DisplayName("Date of Join")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of Join is required")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", NullDisplayText = "")]
        //[DataType(DataType.Date)]
        public DateTime DateOfJoin { get; set; }

        [DisplayName("Last Working Day")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", NullDisplayText = "")]
        //[DataType(DataType.Date)]
        public DateTime? LastWorkingDay { get; set; }

        [DisplayName("Primary Skills")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Primary Skills is required")]
        [MaxLength(100, ErrorMessage = "Primary Skills should not exceed 100 characters")]
        public string PrimarySkills { get; set; }

        [DisplayName("Secondary Skills")]
        [MaxLength(100, ErrorMessage = "Secondary Skills should not exceed 100 characters")]
        public string SecondarySkills { get; set; }

        [DisplayName("Reporting Manager")]
        public int? ReportingManagerID { get; set; }

        [DisplayName("Reporting Manager")]
        public string ReportingManagerName { get; set; }

        [DisplayName("Project Manager")]
        public int? ProjectManagerID { get; set; }

        [DisplayName("Project Manager")]
        public string ProjectManagerName { get; set; }

        [DisplayName("Utilization Type")]
        public int? UtilizationTypeID { get; set; }

        [DisplayName("Utilization Type")]
        public string UtilizationTypeName { get; set; }

        [DisplayName("Employment Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select an Employment Type")]
        public int EmploymentTypeID { get; set; }

        [DisplayName("Employment Type")]
        public string EmploymentTypeName { get; set; }

        public List<EmployeePodAllocationModel> PodAllocations { get; set; }

        #region POD Allocations

        [DisplayName("POD Name")]
        public int PodID1 { get; set; }

        [DisplayName("Allocation %")]
        public int PodAllocation1 { get; set; }

        [DisplayName("POD Name")]
        public int? PodID2 { get; set; }

        [DisplayName("Allocation %")]
        public int? PodAllocation2 { get; set; }

        [DisplayName("POD Name")]
        public int? PodID3 { get; set; }

        [DisplayName("Allocation %")]
        public int? PodAllocation3 { get; set; }

        [DisplayName("POD Name")]
        public int? PodID4 { get; set; }

        [DisplayName("Allocation %")]
        public int? PodAllocation4 { get; set; }

        #endregion
    }
}