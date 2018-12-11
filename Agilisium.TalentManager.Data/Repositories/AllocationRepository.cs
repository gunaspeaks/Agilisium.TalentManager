using Agilisium.TalentManager.Data.Abstract;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Data.Repositories
{
    public class AllocationRepository : RepositoryBase<ProjectAllocation>, IAllocationRepository
    {
        public void Add(ProjectAllocationDto entity)
        {
            ProjectAllocation allocation = CreateBusinessEntity(entity, true);
            Entities.Add(allocation);
            DataContext.Entry(allocation).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(ProjectAllocationDto entity)
        {
            ProjectAllocation allocation = Entities.FirstOrDefault(e => e.AllocationEntryID == entity.AllocationEntryID);
            allocation.IsDeleted = true;
            allocation.UpdateEntityBase(entity.LoggedInUserName);
            DataContext.Entry(allocation).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(int empEntryID, int projectID)
        {
            return Entities.Any(a => a.EmployeeID == empEntryID &&
            a.ProjectID == projectID && a.IsDeleted == false);
        }

        public bool Exists(int allocationID, int empEntryID, int projectID)
        {
            return Entities.Any(a => a.AllocationEntryID != allocationID &&
            a.EmployeeID == empEntryID && a.ProjectID == projectID && a.IsDeleted == false);
        }

        public bool Exists(int id)
        {
            return Entities.Any(a => a.AllocationEntryID == id && a.IsDeleted == false);
        }

        public bool Exists(string projectName)
        {
            return (from a in Entities
                    join p in DataContext.Projects on a.ProjectID equals p.ProjectID into pe
                    from ped in pe.DefaultIfEmpty()
                    where ped.ProjectName == projectName && a.IsDeleted == false && ped.IsDeleted == false
                    select a).Any();
        }

        public IEnumerable<ProjectAllocationDto> GetAll()
        {
            return (from p in Entities
                    join em in DataContext.Employees on p.EmployeeID equals em.EmployeeEntryID into eme
                    from emd in eme.DefaultIfEmpty()
                    join sc in DataContext.DropDownSubCategories on p.AllocationTypeID equals sc.SubCategoryID into sce
                    from scd in sce.DefaultIfEmpty()
                    join pr in DataContext.Projects on p.ProjectID equals pr.ProjectID into pre
                    from prd in pre.DefaultIfEmpty()
                    where p.IsDeleted == false && emd.IsDeleted == false && scd.IsDeleted == false && prd.IsDeleted == false
                    select new ProjectAllocationDto
                    {
                        AllocationEndDate = p.AllocationEndDate,
                        AllocationEntryID = p.AllocationEntryID,
                        AllocationStartDate = p.AllocationStartDate,
                        AllocationTypeID = p.AllocationTypeID,
                        AllocationTypeName = scd.SubCategoryName,
                        EmployeeName = emd.LastName + ", " + emd.FirstName,
                        ProjectName = prd.ProjectName,
                        EmployeeID = p.EmployeeID,
                        ProjectID = p.ProjectID,
                        Remarks = p.Remarks,
                        PercentageOfAllocation = p.PercentageOfAllocation
                    });
        }

        public ProjectAllocationDto GetByID(int id)
        {
            return (from p in Entities
                    where p.AllocationEntryID == id
                    select new ProjectAllocationDto
                    {
                        AllocationEndDate = p.AllocationEndDate,
                        AllocationEntryID = p.AllocationEntryID,
                        AllocationStartDate = p.AllocationStartDate,
                        AllocationTypeID = p.AllocationTypeID,
                        EmployeeID = p.EmployeeID,
                        ProjectID = p.ProjectID,
                        Remarks = p.Remarks,
                        PercentageOfAllocation = p.PercentageOfAllocation
                    }).FirstOrDefault();
        }

        public void Update(ProjectAllocationDto entity)
        {
            ProjectAllocation buzEntity = Entities.FirstOrDefault(e => e.AllocationEntryID == entity.AllocationEntryID);
            MigrateEntity(entity, buzEntity);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        private ProjectAllocation CreateBusinessEntity(ProjectAllocationDto projectDto, bool isNewEntity = false)
        {
            ProjectAllocation entity = new ProjectAllocation
            {
                AllocationEndDate = projectDto.AllocationEndDate,
                AllocationStartDate = projectDto.AllocationStartDate,
                AllocationTypeID = projectDto.AllocationTypeID,
                EmployeeID = projectDto.EmployeeID,
                ProjectID = projectDto.ProjectID,
                PercentageOfAllocation = projectDto.PercentageOfAllocation,
            };

            if (isNewEntity == false)
            {
                entity.AllocationEntryID = projectDto.AllocationEntryID;
            }

            entity.UpdateEntityBase(projectDto.LoggedInUserName, isNewEntity: true);
            return entity;
        }

        private void MigrateEntity(ProjectAllocationDto sourceEntity, ProjectAllocation destinationEntity)
        {
            destinationEntity.AllocationEndDate = sourceEntity.AllocationEndDate;
            destinationEntity.AllocationStartDate = sourceEntity.AllocationStartDate;
            destinationEntity.AllocationTypeID = sourceEntity.AllocationTypeID;
            destinationEntity.EmployeeID = sourceEntity.EmployeeID;
            destinationEntity.ProjectID = sourceEntity.ProjectID;
            destinationEntity.PercentageOfAllocation = sourceEntity.PercentageOfAllocation;
            destinationEntity.UpdateEntityBase(sourceEntity.LoggedInUserName);
        }

        public int GetPercentageOfAllocation(int employeeID, int projectID)
        {
            return (from a in Entities
                    where a.EmployeeID == employeeID && a.ProjectID != projectID
                    select a.PercentageOfAllocation).Sum();
        }
    }

    public interface IAllocationRepository : IRepository<ProjectAllocationDto>
    {
        bool Exists(int empEntryID, int projectID);

        bool Exists(int allocationID, int empEntryID, int projectID);

        int GetPercentageOfAllocation(int employeeID, int projectID);
    }
}
