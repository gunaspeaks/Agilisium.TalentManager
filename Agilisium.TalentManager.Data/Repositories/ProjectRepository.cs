﻿using Agilisium.TalentManager.Data.Abstract;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Data.Repositories
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

        public IEnumerable<ProjectDto> GetAll(int pageSize, int pageNo = -1)
        {
            return (from p in Entities
                    orderby p.ProjectName
                    where p.IsDeleted == false
                    join dm in DataContext.Employees on p.DeliveryManagerID equals dm.EmployeeEntryID into dme
                    from dmd in dme.DefaultIfEmpty()
                    join pr in DataContext.Practices on p.PraticeID equals pr.PracticeID into pre
                    from prd in pre.DefaultIfEmpty()
                    join pt in DataContext.DropDownSubCategories on p.ProjectTypeID equals pt.SubCategoryID into pte
                    from ptd in pte.DefaultIfEmpty()
                    join sp in DataContext.SubPractices on p.SubPracticeID equals sp.SubPracticeID into spe
                    from spd in spe.DefaultIfEmpty()
                    select new ProjectDto
                    {
                        DeliveryManagerID = p.DeliveryManagerID,
                        DeliveryManagerName = dmd.LastName + ", " + dmd.FirstName,
                        EndDate = p.EndDate,
                        PracticeName = prd.PracticeName,
                        PraticeID = p.PraticeID,
                        ProjectCode = p.ProjectCode,
                        ProjectID = p.ProjectID,
                        ProjectManagerID = p.ProjectManagerID,
                        ProjectName = p.ProjectName,
                        ProjectTypeID = p.ProjectTypeID,
                        ProjectTypeName = ptd.SubCategoryName,
                        SubPracticeID = p.SubPracticeID,
                        Remarks = p.Remarks,
                        StartDate = p.StartDate,
                        SubPracticeName = spd.SubPracticeName
                    });
        }

        public ProjectDto GetByID(int id)
        {
            return (from p in Entities
                    orderby p.ProjectName
                    where p.ProjectID == id && p.IsDeleted == false
                    select new ProjectDto
                    {
                        DeliveryManagerID = p.DeliveryManagerID,
                        EndDate = p.EndDate,
                        PraticeID = p.PraticeID,
                        ProjectCode = p.ProjectCode,
                        ProjectID = p.ProjectID,
                        ProjectManagerID = p.ProjectManagerID,
                        ProjectName = p.ProjectName,
                        ProjectTypeID = p.ProjectTypeID,
                        SubPracticeID = p.SubPracticeID,
                        Remarks = p.Remarks,
                        StartDate = p.StartDate,
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
                PraticeID = projectDto.PraticeID,
                ProjectCode = projectDto.ProjectCode,
                ProjectManagerID = projectDto.ProjectManagerID,
                ProjectName = projectDto.ProjectName,
                ProjectTypeID = projectDto.ProjectTypeID,
                Remarks = projectDto.Remarks,
                StartDate = projectDto.StartDate,
                SubPracticeID = projectDto.SubPracticeID,
                ProjectID = projectDto.ProjectID
            };

            entity.UpdateTimeStamp(projectDto.LoggedInUserName, true);
            return entity;
        }

        private void MigrateEntity(ProjectDto sourceEntity, Project targetEntity)
        {
            targetEntity.DeliveryManagerID = sourceEntity.DeliveryManagerID;
            targetEntity.EndDate = sourceEntity.EndDate;
            targetEntity.PraticeID = sourceEntity.PraticeID;
            targetEntity.ProjectCode = sourceEntity.ProjectCode;
            targetEntity.ProjectManagerID = sourceEntity.ProjectManagerID;
            targetEntity.ProjectName = sourceEntity.ProjectName;
            targetEntity.ProjectTypeID = sourceEntity.ProjectTypeID;
            targetEntity.Remarks = sourceEntity.Remarks;
            targetEntity.StartDate = sourceEntity.StartDate;
            targetEntity.SubPracticeID = sourceEntity.SubPracticeID;
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
