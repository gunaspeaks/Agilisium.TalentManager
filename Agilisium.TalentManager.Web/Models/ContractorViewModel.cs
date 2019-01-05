using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Agilisium.TalentManager.Web.Models
{
    public class ContractorViewModel : ViewModelBase
    {
        public ContractorViewModel()
        {
            Contractors = new List<ContractorModel>();
        }

        public List<ContractorModel> Contractors { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }

    public class ContractorModel : ViewModelBase
    {
        public int ContractorID { get; set; }

        [DisplayName("Project Name")]
        public int ProjectID { get; set; }

        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [DisplayName("Contractor Name")]
        public string ContractorName { get; set; }

        public int AgilisiumManagerID { get; set; }

        [DisplayName("Agilisium Manager")]
        public string AgilisiumManagerName { get; set; }

        [DisplayName("Vendor")]
        public int VendorID { get; set; }

        [DisplayName("Vendor")]
        public string VendorName { get; set; }

        public string SkillSet { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Billing Rate")]
        public double BillingRate { get; set; }

        [DisplayName("Client Rate")]
        public double ClientRate { get; set; }

        [DisplayName("Onshore Rate")]
        public double OnshoreRate { get; set; }

        [DisplayName("Contract Period")]
        public int ContractPeriodID { get; set; }

        [DisplayName("Contract Period")]
        public string ContractPeriod { get; set; }
    }
}