using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repository;

        public bool Exists(int employeeEntryID)
        {
            return repository.Exists(employeeEntryID);
        }

        public EmployeeService(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public void Create(EmployeeDto employee)
        {
            repository.Add(employee);
        }

        public void Delete(EmployeeDto employee)
        {
            repository.Delete(employee);
        }

        public List<EmployeeDto> GetAllEmployees(int pageSize = 0, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo).ToList();
        }

        public EmployeeDto GetEmployee(int id)
        {
            return repository.GetByID(id);
        }

        public bool IsDuplicateEmployeeID(string employeeID)
        {
            return repository.IsDuplicateEmployeeID(employeeID);
        }

        public bool IsDuplicateName(string firstName, string lastName)
        {
            return repository.IsDuplicateName(firstName, lastName);
        }

        public bool IsDuplicateName(int employeeEntryID, string firstName, string lastName)
        {
            return repository.IsDuplicateName(employeeEntryID, firstName, lastName);
        }

        public void Update(EmployeeDto employee)
        {
            repository.Update(employee);
        }

        public bool IsDuplicateEmployeeID(int employeeEntryID, string employeeID)
        {
            return repository.IsDuplicateEmployeeID(employeeEntryID, employeeID);
        }

        public string GenerateNewEmployeeID(int trackerID)
        {
            return repository.GenerateNewEmployeeID(trackerID);
        }

        public List<EmployeeDto> GetAllManagers()
        {
            return repository.GetAllManagers().ToList();
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }
    }
}
