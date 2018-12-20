using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class ProjectModel : ViewModelBase
    {
        [DisplayName("Project ID")]
        public int ProjectID { get; set; }

        [DisplayName("Project Name")]
        [Required(ErrorMessage ="Project Name is required")]
        public string ProjectName { get; set; }

        [DisplayName("Project Code")]
        [Required(ErrorMessage ="Project Code is required")]
        public string ProjectCode { get; set; }

        [DisplayName("Delivery Manager")]
        public int? DeliveryManagerID { get; set; }

        [DisplayName("Delivery Manager")]
        public string DeliveryManagerName { get; set; }

        [DisplayName("Project Manager")]
        [Required(ErrorMessage ="Please select a Project Manager")]
        public int ProjectManagerID { get; set; }

        [DisplayName("Project Manager")]
        public string ProjectManagerName { get; set; }

        [DisplayName("Project Type")]
        [Required(ErrorMessage = "Please select a Project Type")]
        public int ProjectTypeID { get; set; }

        [DisplayName("Project Type")]
        public string ProjectTypeName { get; set; }

        [DisplayName("Start Date")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Project Start Date is required")]
        //[DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [DisplayName("Practice")]
        [Required(ErrorMessage = "Please select a Practice")]
        public int PracticeID { get; set; }

        [DisplayName("Practice")]
        public string PracticeName { get; set; }

        [DisplayName("Sub Practice")]
        public int? SubPracticeID { get; set; }

        [DisplayName("Sub Practice")]
        public string SubPracticeName { get; set; }

        [DisplayName("Is SoW Available")]
        public bool IsSowAvailable { get; set; }

        [DisplayName("SoW Started On")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime? SowStartDate { get; set; }

        [DisplayName("SoW Completed By")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime? SowEndDate { get; set; }

    }
}