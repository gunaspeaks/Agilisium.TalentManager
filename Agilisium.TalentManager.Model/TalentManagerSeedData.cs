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
            GetSubCategories().ForEach(c => context.DropDownSubCategories.Add(c));
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
            };
        }

        private static List<DropDownSubCategory> GetSubCategories()
        {
            return new List<DropDownSubCategory>
            {
                // Business Unit - Sub Categories
                new DropDownSubCategory
                {
                    SubCategoryName = "Business Development",
                    ShortName = "BD",
                    CategoryID = 1,
                    Description = "Business Development",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Business Operations",
                    ShortName = "BO",
                    CategoryID = 1,
                    Description = "Business Operations",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Delivery",
                    ShortName = "DL",
                    CategoryID = 1,
                    Description = "Project Delivery",
                    IsReserved = true
                },

                // Utilization Codes
                new DropDownSubCategory
                {
                    SubCategoryName = "Billable",
                    ShortName = "BL",
                    CategoryID = 2,
                    Description = "Billable",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Shadow",
                    ShortName = "SHD",
                    CategoryID = 2,
                    Description = "Shadow",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Bench",
                    ShortName = "BCH",
                    CategoryID = 2,
                    Description = "Bench",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Internal",
                    ShortName = "INT",
                    CategoryID = 2,
                    Description = "Internal",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Lab",
                    ShortName = "LAB",
                    CategoryID = 2,
                    Description = "Lab",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Proposed-Awaiting Start",
                    ShortName = "PAS",
                    CategoryID = 2,
                    Description = "Proposed-Awaiting Start",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Not In Payroll",
                    ShortName = "NIP",
                    CategoryID = 2,
                    Description = "Not In Payroll",
                    IsReserved = true
                },
                // Project Type
                new DropDownSubCategory
                {
                    SubCategoryName = "Client Billable",
                    ShortName = "CBL",
                    CategoryID = 3,
                    Description = "Client Billable",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Internal Project",
                    ShortName = "IPR",
                    CategoryID = 3,
                    Description = "Internal Project",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Lab",
                    ShortName = "Lab",
                    CategoryID = 3,
                    Description = "Lab",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Proposed",
                    ShortName = "PSD",
                    CategoryID = 3,
                    Description = "Proposed",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Long Leave",
                    ShortName = "LNL",
                    CategoryID = 3,
                    Description = "Long Leave",
                    IsReserved = true
                },

                // Employment Type
                new DropDownSubCategory
                {
                    SubCategoryName = "Permanent",
                    ShortName = "PMT",
                    CategoryID = 4,
                    Description = "Permanent employee",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Internship",
                    ShortName = "INT",
                    CategoryID = 4,
                    Description = "Internship employee",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Contract",
                    ShortName = "CNT",
                    CategoryID = 4,
                    Description = "Contract employee",
                    IsReserved = true
                }
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
