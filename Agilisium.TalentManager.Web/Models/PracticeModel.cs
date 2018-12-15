using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class PracticeModel : ViewModelBase
    {
        public int PracticeID { get; set; }

        [DisplayName("Practice Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Practice Name is required")]
        [MaxLength(100, ErrorMessage = "Practice Name should not exceed 100 characters")]
        public string PracticeName { get; set; }

        [DisplayName("Short Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Short Name is required")]
        [MaxLength(10, ErrorMessage = "Short Name should not should not exceed 10 characters")]
        public string ShortName { get; set; }

        public bool IsReserved { get; set; }
    }
}