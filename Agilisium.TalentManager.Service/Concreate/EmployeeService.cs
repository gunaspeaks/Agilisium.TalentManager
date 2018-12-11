using Agilisium.TalentManager.Data.Repositories;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public void Create(EmployeeDto employee)
        {
            repository.Add(employee);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            return repository.GetAll().ToList();
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
    }
}
