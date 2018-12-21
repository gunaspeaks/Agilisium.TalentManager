﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Web.Models
{
    public class PracticeModel : ViewModelBase
    {
        public int PracticeID { get; set; }

        [DisplayName("Business Unit")]
        [Required(ErrorMessage = "Please select a Business Unit")]
        public int BusinessUnitID { get; set; }

        [DisplayName("Business Unit")]
        public string BusinessUnitName { get; set; }

        [DisplayName("Practice Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Practice Name is required")]
        [MaxLength(100, ErrorMessage = "Practice Name should not exceed 100 characters")]
        public string PracticeName { get; set; }

        [DisplayName("Short Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Short Name is required")]
        [MaxLength(10, ErrorMessage = "Short Name should not should not exceed 10 characters")]
        public string ShortName { get; set; }

        [DisplayName("Practice Manager")]
        public int? ManagerID { get; set; }

        [DisplayName("Practice Manager")]
        public string ManagerName { get; set; }

        public bool IsReserved { get; set; }
    }
}