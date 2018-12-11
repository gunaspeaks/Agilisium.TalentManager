using Agilisium.TalentManager.Dto;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IProjectService
    {
        void Create(ProjectDto project);

        void Update(ProjectDto project);

        void Delete(int id);

        bool Exists(string projectName, int id);

        bool Exists(string itemName);

        bool Exists(int id);

        IEnumerable<ProjectDto> GetAll();

        ProjectDto GetByID(int id);

        bool IsDuplicateProjectCode(string projectCode);

        bool IsDuplicateProjectCode(string projectCode, int projectID);
    }
}
