using Agilisium.TalentManager.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Model
{
    public class TalentManagerSeedData : DropCreateDatabaseIfModelChanges<TalentManagerDataContext>
    {
        protected override void Seed(TalentManagerDataContext context)
        {
            GetCategories().ForEach(c => context.DropDownCategories.Add(c));
            context.SaveChanges();

            GetSubCategories(context).ForEach(c => context.DropDownSubCategories.Add(c));
            context.SaveChanges();

            GetPractices(context).ForEach(p => context.Practices.Add(p));
            context.SaveChanges();

            GetEmployeeIDTrackers(context).ForEach(e => context.EmployeeIDTrackers.Add(e));
            context.SaveChanges();
        }

        private static List<DropDownCategory> GetCategories()
        {
            return new List<DropDownCategory>() {
                new DropDownCategory
                {
                     CategoryName = "Business Unit",
                     Description = "Business Unit",
                     ShortName = "BU",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Utilization Type",
                     Description = "Resource utilization types",
                     ShortName = "UT",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Project Type",
                     Description = "Project Types",
                     ShortName = "PT",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Employment Type",
                     Description = "Employment Types",
                     ShortName = "ET",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Specialized Partner",
                     Description = "Specialized Partner",
                     ShortName = "SP",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Contract Period",
                     Description = "Contract Period",
                     ShortName = "CP",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Service Request Status",
                     Description = "Service Request Status",
                     ShortName = "SR",
                     IsReserved = true
                },
            };
        }

        private static List<DropDownSubCategory> GetSubCategories(TalentManagerDataContext context)
        {
            int buCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Business Unit").CategoryID;
            int utCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Utilization Type").CategoryID;
            int ptCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Project Type").CategoryID;
            int etCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Employment Type").CategoryID;
            int spCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Specialized Partner").CategoryID;
            int cpCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Contract Period").CategoryID;
            int srCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Service Request Status").CategoryID;

            return new List<DropDownSubCategory>
            {
                // Business Unit - Sub Categories
                new DropDownSubCategory
                {
                    SubCategoryName = "Business Development",
                    ShortName = "BD",
                    CategoryID = buCID,
                    Description = "Business Development",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Business Operations",
                    ShortName = "BO",
                    CategoryID = buCID,
                    Description = "Business Operations",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Delivery",
                    ShortName = "DL",
                    CategoryID = buCID,
                    Description = "Project Delivery",
                    IsReserved = true
                },

                // Utilization Codes
                new DropDownSubCategory
                {
                    SubCategoryName = "Billable",
                    ShortName = "BL",
                    CategoryID = utCID,
                    Description = "Billable",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Shadow",
                    ShortName = "SHD",
                    CategoryID = utCID,
                    Description = "Shadow",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Bench",
                    ShortName = "BCH",
                    CategoryID = utCID,
                    Description = "Bench",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Internal",
                    ShortName = "INT",
                    CategoryID = utCID,
                    Description = "Internal",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Lab",
                    ShortName = "LAB",
                    CategoryID = utCID,
                    Description = "Lab",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Proposed-Awaiting Start",
                    ShortName = "PAS",
                    CategoryID = utCID,
                    Description = "Proposed-Awaiting Start",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Not In Payroll",
                    ShortName = "NIP",
                    CategoryID = utCID,
                    Description = "Not In Payroll",
                    IsReserved = true
                },

                // Project Type
                new DropDownSubCategory
                {
                    SubCategoryName = "Client Billable",
                    ShortName = "CBL",
                    CategoryID = ptCID,
                    Description = "Client Billable",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Internal Project",
                    ShortName = "IPR",
                    CategoryID = ptCID,
                    Description = "Internal Project",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Lab",
                    ShortName = "Lab",
                    CategoryID = ptCID,
                    Description = "Lab",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Proposed",
                    ShortName = "PSD",
                    CategoryID = ptCID,
                    Description = "Proposed",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Long Leave",
                    ShortName = "LNL",
                    CategoryID = ptCID,
                    Description = "Long Leave",
                    IsReserved = true
                },

                // Employment Type
                new DropDownSubCategory
                {
                    SubCategoryName = "Permanent",
                    ShortName = "PMT",
                    CategoryID = etCID,
                    Description = "Permanent employee",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Internship",
                    ShortName = "INT",
                    CategoryID = etCID,
                    Description = "Internship employee",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Contract",
                    ShortName = "CNT",
                    CategoryID = etCID,
                    Description = "Contract employee",
                    IsReserved = true
                },
      
                // Specialized Partner
                new DropDownSubCategory
                {
                    SubCategoryName = "Microsoft",
                    ShortName = "MFT",
                    CategoryID = spCID,
                    Description = "Microsoft",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Google",
                    ShortName = "GGL",
                    CategoryID = spCID,
                    Description = "Google",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "AWS",
                    ShortName = "AWS",
                    CategoryID = spCID,
                    Description = "AWS",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Databricks",
                    ShortName = "DBK",
                    CategoryID = spCID,
                    Description = "Databricks",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Snaplogic",
                    ShortName = "SNP",
                    CategoryID = spCID,
                    Description = "Snaplogic",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Others",
                    ShortName = "OTH",
                    CategoryID = spCID,
                    Description = "Others",
                    IsReserved = true
                },
            
                // Contract Period
                new DropDownSubCategory
                {
                    SubCategoryName = "1 Month",
                    ShortName = "OMN",
                    CategoryID = cpCID,
                    Description = "1 Month",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "2 Months",
                    ShortName = "TMN",
                    CategoryID = cpCID,
                    Description = "2 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "3 Months",
                    ShortName = "THM",
                    CategoryID = cpCID,
                    Description = "3 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "4 Months",
                    ShortName = "FMN",
                    CategoryID = cpCID,
                    Description = "4 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "5 Months",
                    ShortName = "FVM",
                    CategoryID = cpCID,
                    Description = "5 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "6 Months",
                    ShortName = "SMN",
                    CategoryID = cpCID,
                    Description = "6 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "7 Months",
                    ShortName = "SVM",
                    CategoryID = cpCID,
                    Description = "7 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "8 Months",
                    ShortName = "EVM",
                    CategoryID = cpCID,
                    Description = "8 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "9 Months",
                    ShortName = "NVM",
                    CategoryID = cpCID,
                    Description = "9 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "10 Months",
                    ShortName = "TNM",
                    CategoryID = cpCID,
                    Description = "10 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "11 Months",
                    ShortName = "ELM",
                    CategoryID = cpCID,
                    Description = "11 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "One Year",
                    ShortName = "OYM",
                    CategoryID = cpCID,
                    Description = "One Year",
                    IsReserved = true
                },

                // Service Request Status - Sub Categories
                new DropDownSubCategory
                {
                    SubCategoryName = "Email Sent",
                    ShortName = "RS",
                    CategoryID = srCID,
                    Description = "Request Sent",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Accepted By Vendor",
                    ShortName = "AV",
                    CategoryID = srCID,
                    Description = "Accepted By Vendor",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Rejected By Vendor",
                    ShortName = "AV",
                    CategoryID = srCID,
                    Description = "Rejected By Vendor",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Waiting for Vendor",
                    ShortName = "AV",
                    CategoryID = srCID,
                    Description = "Waiting for Vendor",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Cancelled",
                    ShortName = "CL",
                    CategoryID = srCID,
                    Description = "Cancelled",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Fulfilled",
                    ShortName = "FF",
                    CategoryID = srCID,
                    Description = "Fulfilled",
                    IsReserved = true
                },
            };
        }

        private static List<Practice> GetPractices(TalentManagerDataContext context)
        {
            int bdID = context.DropDownSubCategories.FirstOrDefault(s => s.SubCategoryName == "Business Development").SubCategoryID;
            int boID = context.DropDownSubCategories.FirstOrDefault(s => s.SubCategoryName == "Business Operations").SubCategoryID;
            int dlID = context.DropDownSubCategories.FirstOrDefault(s => s.SubCategoryName == "Delivery").SubCategoryID;

            return new List<Practice>
            {
                new Practice
                {
                    PracticeName = "Marketing",
                    ShortName = "MKT",
                    BusinessUnitID = bdID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Sales",
                    ShortName = "SLS",
                    BusinessUnitID = bdID,
                    IsReserved = true
                },
                 new Practice
                {
                    PracticeName = "Pre-Sales",
                    ShortName = "PRS",
                    BusinessUnitID = bdID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "CEO",
                    ShortName = "CEO",
                    BusinessUnitID = boID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Operations",
                    ShortName = "OPT",
                    BusinessUnitID = boID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Ops-PMO",
                    ShortName = "OPP",
                    BusinessUnitID = boID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Admin",
                    ShortName = "ADM",
                    BusinessUnitID = boID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Finance",
                    ShortName = "FIN",
                    BusinessUnitID = boID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "People Practice",
                    ShortName = "PPL",
                    BusinessUnitID = boID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "ITS",
                    ShortName = "ITS",
                    BusinessUnitID = boID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "AWS",
                    ShortName = "AWS",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Data Engineering",
                    ShortName = "DEN",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Project Management",
                    ShortName = "PM",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Testing",
                    ShortName = "TST",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Microsoft",
                    ShortName = "Google",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Google",
                    ShortName = "GOG",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Prod. Support",
                    ShortName = "PSP",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "BI",
                    ShortName = "BI",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Data Science",
                    ShortName = "DTS",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Others",
                    ShortName = "OTH",
                    BusinessUnitID = dlID,
                    IsReserved = true
                }
            };
        }

        private static List<EmployeeIDTracker> GetEmployeeIDTrackers(TalentManagerDataContext context)
        {
            int? empTypeCategory = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Employment Type")?.CategoryID;
            List<DropDownSubCategory> empTypes = context.DropDownSubCategories.Where(c => c.CategoryID == empTypeCategory).ToList();

            int? contracEmpTypeID = empTypes.FirstOrDefault(c => c.SubCategoryName == "Contract")?.SubCategoryID;
            int? permanentEmpTypeID = empTypes.FirstOrDefault(c => c.SubCategoryName == "Permanent")?.SubCategoryID;
            int? internshipEmpTypeID = empTypes.FirstOrDefault(c => c.SubCategoryName == "Internship")?.SubCategoryID;

            return new List<EmployeeIDTracker>
            {
                new EmployeeIDTracker
                {
                    EmploymentTypeID = permanentEmpTypeID.Value,
                    IDPrefix = string.Empty,
                    RunningID = 10000
                },
                new EmployeeIDTracker
                {
                    EmploymentTypeID = internshipEmpTypeID.Value,
                    IDPrefix = "CI",
                    RunningID = 1000
                },
                new EmployeeIDTracker
                {
                    EmploymentTypeID = contracEmpTypeID.Value,
                    IDPrefix = "CE",
                    RunningID = 1000
                }
            };
        }
    }
}
