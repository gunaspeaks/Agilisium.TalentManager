using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class SubPracticeViewModel : ViewModelBase
    {
        public int SubPracticeID { get; set; }

        [DisplayName("Sub Practice Name")]
        [Required(ErrorMessage ="Sub Practice Name is required", AllowEmptyStrings =false)]
        [MaxLength(100, ErrorMessage = "Sub Practice Name should not exceed 100 characters")]
        public string SubPracticeName { get; set; }

        [DisplayName("Short Name")]
        [Required(ErrorMessage = "Short Name is required", AllowEmptyStrings = false)]
        [MaxLength(100, ErrorMessage = "Short Name should not exceed 10 characters")]
        public string ShortName { get; set; }

        [Required(ErrorMessage ="Practice is required")]
        public int PracticeID { get; set; }

        [DisplayName("Practice Name")]
        public string PracticeName { get; set; }

        public bool IsReserved { get; set; }
    }
}