using Agilisium.TalentManager.Dto;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetAllEmployees(int pageSize = 0, int pageNo = -1);

        EmployeeDto GetEmployee(int id);

        void Create(EmployeeDto employee);

        void Update(EmployeeDto employee);

        void Delete(EmployeeDto employee);

        bool IsDuplicateName(string firstName, string lastName);

        bool IsDuplicateName(int employeeEntryID, string firstName, string lastName);

        bool IsDuplicateEmployeeID(string employeeID);

        bool IsDuplicateEmployeeID(int employeeEntryID, string employeeID);

        string GenerateNewEmployeeID(int trackerID);

        List<EmployeeDto> GetAllManagers();
    }
}
