using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class ContractorWidgetModel
    {
        [DisplayName("Total Contractors")]
        public int TotalContractors { get; set; }
    }
}