using Agilisium.TalentManager.Data.Repositories;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository repository;

        public ProjectService(IProjectRepository repository)
        {
            this.repository = repository;
        }

        public void Create(ProjectDto project)
        {
            repository.Add(project);
        }

        public void Delete(ProjectDto project)
        {
            repository.Delete(project);
        }

        public bool Exists(string projectName, int id)
        {
            return repository.Exists( id, projectName);
        }

        public bool Exists(string projectName)
        {
            return repository.Exists(projectName);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public IEnumerable<ProjectDto> GetAll()
        {
            return repository.GetAll();
        }

        public ProjectDto GetByID(int id)
        {
            return repository.GetByID(id);
        }

        public void Update(ProjectDto project)
        {
            repository.Update(project);
        }

        public bool IsDuplicateProjectCode(string projectCode)
        {
            return repository.IsDuplicateProjectCode(projectCode);
        }

        public bool IsDuplicateProjectCode(string projectCode, int projectID)
        {
            return repository.IsDuplicateProjectCode(projectCode, projectID);
        }
    }
}
