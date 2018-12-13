using Agilisium.TalentManager.Model.Configuration;
using Agilisium.TalentManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Model
{
    public class TalentManagerDataContext:DbContext
    {
        public TalentManagerDataContext():base("TalentDataContext")
        {
        }

        public DbSet<Practice> Practices { get; set; }

        public DbSet<SubPractice> SubPractices { get; set; }

        public DbSet<DropDownCategory> DropDownCategories { get; set; }

        public DbSet<DropDownSubCategory> DropDownSubCategories { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectAllocation> ProjectAllocations { get; set; }

        public DbSet<EmployeeIDTracker> EmployeeIDTrackers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PracticeEntityConfiguration());
            modelBuilder.Configurations.Add(new SubPracticeEntityConfiguration());
            modelBuilder.Configurations.Add(new DropDownCategoryConfiguration());
            modelBuilder.Configurations.Add(new DropDownSubCategoryConfiguration());
            modelBuilder.Configurations.Add(new EmployeeEntityConfiguration());
            modelBuilder.Configurations.Add(new ProjectEntityConfiguration());
            modelBuilder.Configurations.Add(new ProjectAllocationEntityConfiguration());
            modelBuilder.Configurations.Add(new EmployeeIDTrackerEntityConfiguration());
        }
    }
}
