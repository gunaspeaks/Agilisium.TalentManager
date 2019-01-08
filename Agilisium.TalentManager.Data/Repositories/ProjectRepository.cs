using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public void Add(ProjectDto entity)
        {
            Project project = CreateBusinessEntity(entity, true);
            Entities.Add(project);
            DataContext.Entry(project).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(ProjectDto entity)
        {
            Project buzEntity = Entities.FirstOrDefault(e => e.ProjectID == entity.ProjectID);
            buzEntity.IsDeleted = true;
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(p => p.ProjectName.ToLower() == itemName.ToLower() && p.IsDeleted == false);
        }

        public bool Exists(int id)
        {
            return Entities.Any(p => p.ProjectID == id && p.IsDeleted == false);
        }

        public bool Exists(int id, string projectName)
        {
            return Entities.Any(p => p.ProjectID != id &&
            p.ProjectName.ToLower() == projectName.ToLower() && p.IsDeleted == false);
        }

        public bool IsDuplicateProjectCode(string projectCode)
        {
            return Entities.Any(p => p.ProjectCode.ToLower() == projectCode.ToLower() && p.IsDeleted == false);
        }

        public bool IsDuplicateProjectCode(string projectCode, int projectID)
        {
            return Entities.Any(p => p.ProjectCode.ToLower() == projectCode.ToLower() &&
            p.ProjectID != projectID && p.IsDeleted == false);
        }

        public IEnumerable<ProjectDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IQueryable<ProjectDto> projects = from p in Entities
                                              where p.IsDeleted == false
                                              join dm in DataContext.Employees on p.DeliveryManagerID equals dm.EmployeeEntryID into dme
                                              from dmd in dme.DefaultIfEmpty()
                                              join pm in DataContext.Employees on p.ProjectManagerID equals pm.EmployeeEntryID into pme
                                              from pmd in pme.DefaultIfEmpty()
                                              join pr in DataContext.Practices on p.PracticeID equals pr.PracticeID into pre
                                              from prd in pre.DefaultIfEmpty()
                                              join pt in DataContext.DropDownSubCategories on p.ProjectTypeID equals pt.SubCategoryID into pte
                                              from ptd in pte.DefaultIfEmpty()
                                              orderby p.ProjectCode
                                              select new ProjectDto
                                              {
                                                  ProjectID = p.ProjectID,
                                                  DeliveryManagerName = string.IsNullOrEmpty(dmd.LastName) ? "" : (dmd.LastName + ", " + dmd.FirstName),
                                                  EndDate = p.EndDate,
                                                  PracticeName = prd.PracticeName,
                                                  ProjectCode = p.ProjectCode,
                                                  ProjectName = p.ProjectName,
                                                  ProjectTypeName = ptd.SubCategoryName,
                                                  Remarks = p.Remarks,
                                                  StartDate = p.StartDate,
                                                  ProjectManagerName = string.IsNullOrEmpty(pmd.LastName) ? "" : pmd.LastName + ", " + pmd.FirstName,
                                                  IsSowAvailable = p.IsSowAvailable,
                                              };

            if (pageSize < 0)
            {
                return projects;
            }
            return projects.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public ProjectDto GetByID(int id)
        {
            return (from p in Entities
                    orderby p.ProjectName
                    join e in DataContext.Employees on p.ProjectManagerID equals e.EmployeeEntryID into ee
                    join pt in DataContext.DropDownSubCategories on p.ProjectTypeID equals pt.SubCategoryID into pte
                    from ptd in pte.DefaultIfEmpty()
                    from ed in ee.DefaultIfEmpty()
                    where p.ProjectID == id && p.IsDeleted == false
                    select new ProjectDto
                    {
                        DeliveryManagerID = p.DeliveryManagerID,
                        EndDate = p.EndDate,
                        PracticeID = p.PracticeID,
                        ProjectCode = p.ProjectCode,
                        ProjectID = p.ProjectID,
                        ProjectManagerID = p.ProjectManagerID,
                        ProjectManagerName = string.IsNullOrEmpty(ed.LastName) ? "" : ed.LastName + ", " + ed.FirstName,
                        ProjectName = p.ProjectName,
                        ProjectTypeID = p.ProjectTypeID,
                        ProjectTypeName = ptd.SubCategoryName,
                        SubPracticeID = p.SubPracticeID,
                        Remarks = p.Remarks,
                        StartDate = p.StartDate,
                        IsSowAvailable = p.IsSowAvailable,
                        SowEndDate = p.SowEndDate,
                        SowStartDate = p.SowStartDate
                    }).FirstOrDefault();
        }

        public void Update(ProjectDto entity)
        {
            Project buzEntity = Entities.FirstOrDefault(p => p.ProjectID == entity.ProjectID);
            MigrateEntity(entity, buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();

        }

        private Project CreateBusinessEntity(ProjectDto projectDto, bool isNewEntity = false)
        {
            Project entity = new Project
            {
                DeliveryManagerID = projectDto.DeliveryManagerID,
                EndDate = projectDto.EndDate,
                PracticeID = projectDto.PracticeID,
                ProjectCode = projectDto.ProjectCode,
                ProjectManagerID = projectDto.ProjectManagerID,
                ProjectName = projectDto.ProjectName,
                ProjectTypeID = projectDto.ProjectTypeID,
                Remarks = projectDto.Remarks,
                StartDate = projectDto.StartDate,
                SubPracticeID = projectDto.SubPracticeID,
                ProjectID = projectDto.ProjectID,
                IsSowAvailable = projectDto.IsSowAvailable,
                SowEndDate = projectDto.SowEndDate,
                SowStartDate = projectDto.SowStartDate
            };

            entity.UpdateTimeStamp(projectDto.LoggedInUserName, true);
            return entity;
        }

        private void MigrateEntity(ProjectDto sourceEntity, Project targetEntity)
        {
            targetEntity.DeliveryManagerID = sourceEntity.DeliveryManagerID;
            targetEntity.EndDate = sourceEntity.EndDate;
            targetEntity.PracticeID = sourceEntity.PracticeID;
            targetEntity.ProjectCode = sourceEntity.ProjectCode;
            targetEntity.ProjectManagerID = sourceEntity.ProjectManagerID;
            targetEntity.ProjectName = sourceEntity.ProjectName;
            targetEntity.ProjectTypeID = sourceEntity.ProjectTypeID;
            targetEntity.Remarks = sourceEntity.Remarks;
            targetEntity.StartDate = sourceEntity.StartDate;
            targetEntity.SubPracticeID = sourceEntity.SubPracticeID;
            targetEntity.IsSowAvailable = sourceEntity.IsSowAvailable;
            targetEntity.SowEndDate = sourceEntity.SowEndDate;
            targetEntity.SowStartDate = sourceEntity.SowStartDate;

            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }
    }

    public interface IProjectRepository : IRepository<ProjectDto>
    {
        bool Exists(int id, string projectName);

        bool IsDuplicateProjectCode(string projectCode);

        bool IsDuplicateProjectCode(string projectCode, int projectID);
    }
}
