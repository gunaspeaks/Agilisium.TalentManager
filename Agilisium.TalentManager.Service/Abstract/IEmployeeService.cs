using Agilisium.TalentManager.Dto;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetAllEmployees();

        EmployeeDto GetEmployee(int id);

        void Create(EmployeeDto employee);

        void Update(EmployeeDto employee);

        void Delete(int id);

        bool IsDuplicateName(string firstName, string lastName);

        bool IsDuplicateName(int employeeEntryID, string firstName, string lastName);

        bool IsDuplicateEmployeeID(string employeeID);

        bool IsDuplicateEmployeeID(int employeeEntryID, string employeeID);
    }
}
