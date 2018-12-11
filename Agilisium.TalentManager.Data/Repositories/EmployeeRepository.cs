using Agilisium.TalentManager.Data.Abstract;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Data.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public bool Exists(string itemName)
        {
            return Entities.Any(e => e.FirstName.ToLower() == itemName.ToLower());
        }

        public bool Exists(int id)
        {
            return Entities.Any(e => e.EmployeeEntryID == id);
        }

        public EmployeeDto GetByID(int id)
        {
            return (from emp in Entities
                    where emp.EmployeeEntryID == id
                    select new EmployeeDto
                    {
                        BusinessUnitID = emp.BusinessUnitID,
                        DateOfJoin = emp.DateOfJoin,
                        EmployeeEntryID = emp.EmployeeEntryID,
                        EmailID = emp.EmailID,
                        EmployeeID = emp.EmployeeID,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        LastWorkingDay = emp.LastWorkingDay,
                        PracticeID = emp.PracticeID,
                        PrimarySkills = emp.PrimarySkills,
                        ProjectManagerID = emp.ProjectManagerID,
                        ReportingManagerID = emp.ReportingManagerID,
                        SecondarySkills = emp.SecondarySkills,
                        SubPracticeID = emp.SubPracticeID,
                        UtilizationTypeID = emp.UtilizationTypeID
                    }).FirstOrDefault();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            return (from emp in Entities
                    join bc in DataContext.DropDownSubCategories on emp.BusinessUnitID equals bc.SubCategoryID into bue
                    from bcd in bue.DefaultIfEmpty()
                    join pc in DataContext.Practices on emp.PracticeID equals pc.PracticeID into pce
                    from pcd in pce.DefaultIfEmpty()
                    join sc in DataContext.SubPractices on emp.SubPracticeID equals sc.SubPracticeID into sce
                    from scd in sce.DefaultIfEmpty()
                    join ut in DataContext.DropDownSubCategories on emp.UtilizationTypeID equals ut.SubCategoryID into ute
                    from utd in ute.DefaultIfEmpty()
                    join rm in Entities on emp.EmployeeID equals rm.EmployeeID into rme
                    from rmd in rme.DefaultIfEmpty()
                    join pm in Entities on emp.EmployeeID equals pm.EmployeeID into pme
                    from pmd in pme.DefaultIfEmpty()

                    select new EmployeeDto
                    {
                        BusinessUnitID = emp.BusinessUnitID,
                        BusinessUnitName = bcd.SubCategoryName,
                        DateOfJoin = emp.DateOfJoin,
                        EmployeeEntryID = emp.EmployeeEntryID,
                        EmailID = emp.EmailID,
                        EmployeeID = emp.EmployeeID,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        LastWorkingDay = emp.LastWorkingDay,
                        PracticeID = emp.PracticeID,
                        PracticeName = pcd.PracticeName,
                        PrimarySkills = emp.PrimarySkills,
                        ProjectManagerID = emp.ProjectManagerID,
                        ProjectManagerName = pmd.LastName + ", " + pmd.FirstName,
                        ReportingManagerID = emp.ReportingManagerID,
                        ReportingManagerName = rmd.LastName + ", " + rmd.FirstName,
                        SecondarySkills = emp.SecondarySkills,
                        SubPracticeID = emp.SubPracticeID,
                        SubPracticeName = scd.SubPracticeName,
                        UtilizationTypeID = emp.UtilizationTypeID,
                        UtilizationTypeName = utd.SubCategoryName
                    });
        }

        public bool IsDuplicateName(string firstName, string lastName)
        {
            return Entities.Any(e =>
                e.FirstName.ToLower() == firstName.ToLower() &&
                e.LastName.ToLower() == lastName.ToLower());
        }

        public bool IsDuplicateName(int employeeEntryID, string firstName, string lastName)
        {
            return Entities.Any(e =>
                e.EmployeeEntryID != employeeEntryID &&
                e.FirstName.ToLower() == firstName.ToLower() &&
                e.LastName.ToLower() == lastName.ToLower());
        }

        public bool IsDuplicateEmployeeID(string employeeID)
        {
            return Entities.Any(e => e.EmployeeID.ToLower() == employeeID.ToLower());
        }

        public void Add(EmployeeDto entity)
        {
            Employee employee = ConvertToEntity(entity, true);
            Entities.Add(employee);
            DataContext.Entry(employee).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Update(EmployeeDto entity)
        {
            Employee employee = ConvertToEntity(entity);
            Entities.Add(employee);
            DataContext.Entry(employee).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Employee entity = Entities.FirstOrDefault(e => e.EmployeeEntryID == id);
            Entities.Remove(entity);
            DataContext.Entry(entity).State = EntityState.Deleted;
            DataContext.SaveChanges();
        }

        public bool IsDuplicateEmployeeID(int employeeEntryID, string employeeID)
        {
            return Entities.Any(e => e.EmployeeID.ToLower() == employeeID.ToLower() && e.EmployeeEntryID != employeeEntryID);
        }

        private Employee ConvertToEntity(EmployeeDto employeeDto, bool isNewEntity = false)
        {
            Employee employee = new Employee
            {
                BusinessUnitID = employeeDto.BusinessUnitID,
                DateOfJoin = employeeDto.DateOfJoin,
                EmailID = employeeDto.EmailID,
                EmployeeID = employeeDto.EmployeeID,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                LastWorkingDay = employeeDto.LastWorkingDay,
                PracticeID = employeeDto.PracticeID,
                PrimarySkills = employeeDto.PrimarySkills,
                ProjectManagerID = employeeDto.ProjectManagerID,
                ReportingManagerID = employeeDto.ReportingManagerID,
                SecondarySkills = employeeDto.SecondarySkills,
                SubPracticeID = employeeDto.SubPracticeID,
                UtilizationTypeID = employeeDto.UtilizationTypeID,
            };

            if (isNewEntity == false)
            {
                employee.EmployeeEntryID = employeeDto.EmployeeEntryID;
            }

            return employee;
        }
    }

    public interface IEmployeeRepository : IRepository<EmployeeDto>
    {
        bool IsDuplicateName(string firstName, string lastName);

        bool IsDuplicateName(int employeeEntryID, string firstName, string lastName);

        bool IsDuplicateEmployeeID(string employeeID);

        bool IsDuplicateEmployeeID(int employeeEntryID, string employeeID);
    }
}
