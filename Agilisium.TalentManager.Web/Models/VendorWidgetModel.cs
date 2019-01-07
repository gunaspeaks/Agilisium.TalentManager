using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class VendorWidgetModel
    {
        [DisplayName("Total Vendors")]
        public int TotalVendors { get; set; }
    }
}