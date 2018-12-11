using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class ProjectViewModel : ViewModelBase
    {
        [DisplayName("Project ID")]
        public int ProjectID { get; set; }

        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [DisplayName("Project Code")]
        public string ProjectCode { get; set; }

        [DisplayName("Delivery Manager")]
        public int DeliveryManagerID { get; set; }

        [DisplayName("Delivery Manager")]
        public string DeliveryManagerName { get; set; }

        [DisplayName("Project Manager")]
        public int ProjectManagerID { get; set; }

        [DisplayName("Project Manager")]
        public string ProjectManagerName { get; set; }

        [DisplayName("Project Type")]
        public int ProjectTypeID { get; set; }

        [DisplayName("Project Type")]
        public string ProjectTypeName { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DisplayName("End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [DisplayName("Practice")]
        public int PracticeID { get; set; }

        [DisplayName("Practice")]
        public string PracticeName { get; set; }

        [DisplayName("Sub Practice")]
        public int SubPracticeID { get; set; }

        [DisplayName("Sub Practice")]
        public string SubPracticeName { get; set; }
    }
}