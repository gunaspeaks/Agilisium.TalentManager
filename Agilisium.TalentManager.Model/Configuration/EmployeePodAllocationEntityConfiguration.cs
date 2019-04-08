using Agilisium.TalentManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Model.Configuration
{
    public class EmployeePodAllocationEntityConfiguration:EntityTypeConfiguration<EmployeePodAllocation>
    {
        public EmployeePodAllocationEntityConfiguration()
        {
            HasKey(k => k.AllocationEntryID);

            Property(p => p.AllocationEntryID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("EmployeePodAllocation");
        }
    }
}
