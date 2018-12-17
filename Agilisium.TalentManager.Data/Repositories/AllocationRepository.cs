using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
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
            allocation.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(allocation);
            DataContext.Entry(allocation).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public int Exists(int empEntryID, int projectID)
        {
            return Entities.Count(a => a.EmployeeID == empEntryID &&
            a.ProjectID == projectID && a.IsDeleted == false);
        }

        public int Exists(int allocationID, int empEntryID, int projectID)
        {
            return Entities.Count(a => a.AllocationEntryID != allocationID &&
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

        public IEnumerable<ProjectAllocationDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IQueryable<ProjectAllocationDto> allocations = from p in Entities
                                                           join em in DataContext.Employees on p.EmployeeID equals em.EmployeeEntryID into eme
                                                           from emd in eme.DefaultIfEmpty()
                                                           join sc in DataContext.DropDownSubCategories on p.AllocationTypeID equals sc.SubCategoryID into sce
                                                           from scd in sce.DefaultIfEmpty()
                                                           join pr in DataContext.Projects on p.ProjectID equals pr.ProjectID into pre
                                                           from prd in pre.DefaultIfEmpty()
                                                           where p.IsDeleted == false
                                                           orderby p.AllocationStartDate
                                                           select new ProjectAllocationDto
                                                           {
                                                               AllocationEndDate = p.AllocationEndDate,
                                                               AllocationEntryID = p.AllocationEntryID,
                                                               AllocationStartDate = p.AllocationStartDate,
                                                               AllocationTypeName = scd.SubCategoryName,
                                                               EmployeeName = emd.LastName + ", " + emd.FirstName,
                                                               EmployeeID = p.EmployeeID,
                                                               ProjectName = prd.ProjectName,
                                                               Remarks = p.Remarks,
                                                               PercentageOfAllocation = p.PercentageOfAllocation
                                                           };

            if (pageSize < 0)
            {
                return allocations;
            }

            return allocations.Skip((pageNo - 1) * pageSize).Take(pageSize);
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
                AllocationEntryID = projectDto.AllocationEntryID
            };

            entity.UpdateTimeStamp(projectDto.LoggedInUserName, isNewEntity: true);
            return entity;
        }

        private void MigrateEntity(ProjectAllocationDto sourceEntity, ProjectAllocation targetEntity)
        {
            targetEntity.AllocationEndDate = sourceEntity.AllocationEndDate;
            targetEntity.AllocationStartDate = sourceEntity.AllocationStartDate;
            targetEntity.AllocationTypeID = sourceEntity.AllocationTypeID;
            targetEntity.EmployeeID = sourceEntity.EmployeeID;
            targetEntity.ProjectID = sourceEntity.ProjectID;
            targetEntity.PercentageOfAllocation = sourceEntity.PercentageOfAllocation;
            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }

        public int GetPercentageOfAllocation(int employeeID, int projectID)
        {
            if (Entities.Any(a => a.ProjectID != projectID && a.EmployeeID == employeeID))
            {
                return Entities.Where(a => a.ProjectID != projectID
                && a.EmployeeID == employeeID).Sum(p => p.PercentageOfAllocation);
            }
            else
            {
                return 0;
            }
        }

        public IEnumerable<string> GetAllocatedProjectsByEmployeeID(int employeeID, int projectToExclude)
        {
            return (from a in Entities
                    join p in DataContext.Projects on a.ProjectID equals p.ProjectID into pe
                    from pd in pe.DefaultIfEmpty()
                    where a.ProjectID != projectToExclude && a.EmployeeID == employeeID
                    orderby pd.ProjectName
                    select pd.ProjectName);
        }
    }

    public interface IAllocationRepository : IRepository<ProjectAllocationDto>
    {
        int Exists(int empEntryID, int projectID);

        int Exists(int allocationID, int empEntryID, int projectID);

        int GetPercentageOfAllocation(int employeeID, int projectID);

        IEnumerable<string> GetAllocatedProjectsByEmployeeID(int employeeID, int projectToExclude);
    }
}
