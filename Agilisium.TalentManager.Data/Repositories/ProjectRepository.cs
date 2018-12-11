using Agilisium.TalentManager.Data.Abstract;
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
            Project project = ConvertToEntity(entity, true);
            Entities.Add(project);
            DataContext.Entry(project).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Project entity = Entities.FirstOrDefault(e => e.ProjectID == id);
            Entities.Remove(entity);
            DataContext.Entry(entity).State = EntityState.Deleted;
            DataContext.SaveChanges();
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(p => p.ProjectName.ToLower() == itemName.ToLower());
        }

        public bool Exists(int id)
        {
            return Entities.Any(p => p.ProjectID == id);
        }

        public bool Exists(int id, string projectName)
        {
            return Entities.Any(p => p.ProjectID != id && p.ProjectName.ToLower() == projectName.ToLower());
        }

        public bool IsDuplicateProjectCode(string projectCode)
        {
            return Entities.Any(p => p.ProjectCode.ToLower() == projectCode.ToLower());
        }

        public bool IsDuplicateProjectCode(string projectCode, int projectID)
        {
            return Entities.Any(p => p.ProjectCode.ToLower() == projectCode.ToLower() && p.ProjectID != projectID);
        }

        public IEnumerable<ProjectDto> GetAll()
        {
            return (from p in Entities
                    orderby p.ProjectName
                    join dm in DataContext.Employees on p.DeliveryManagerID equals dm.EmployeeEntryID into dme
                    from dmd in dme.DefaultIfEmpty()
                    join pr in DataContext.Practices on p.PraticeID equals pr.PracticeID into pre
                    from prd in pre.DefaultIfEmpty()
                    join pm in DataContext.Employees on p.ProjectManagerID equals pm.ProjectManagerID into pme
                    from pmd in pme.DefaultIfEmpty()
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
                        ProjectManagerName = pmd.LastName + ", " + pmd.FirstName,
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
                    where p.ProjectID == id
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
            Project project = ConvertToEntity(entity);
            Entities.Add(project);
            DataContext.Entry(project).State = EntityState.Modified;
            DataContext.SaveChanges();

        }

        private Project ConvertToEntity(ProjectDto projectDto, bool isNewEntity = false)
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
                SubPracticeID = projectDto.SubPracticeID
            };

            if (isNewEntity == false)
            {
                entity.ProjectID = projectDto.ProjectID;
            }

            return entity;
        }
    }

    public interface IProjectRepository : IRepository<ProjectDto>
    {
        bool Exists(int id, string projectName);

        bool IsDuplicateProjectCode(string projectCode);

        bool IsDuplicateProjectCode(string projectCode, int projectID);
    }
}
