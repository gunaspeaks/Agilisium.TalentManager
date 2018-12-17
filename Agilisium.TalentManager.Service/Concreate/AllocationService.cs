using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class AllocationService : IAllocationService
    {
        private readonly IAllocationRepository repository;

        public AllocationService(IAllocationRepository repository)
        {
            this.repository = repository;
        }

        public void Add(ProjectAllocationDto entity)
        {
            repository.Add(entity);
        }

        public void Delete(ProjectAllocationDto entity)
        {
            repository.Delete(entity);
        }

        public int Exists(int empEntryID, int projectID)
        {
            return repository.Exists(empEntryID, projectID);
        }

        public int Exists(int allocationID, int empEntryID, int projectID)
        {
            return repository.Exists(allocationID, empEntryID, projectID);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public bool Exists(string itemName)
        {
            return repository.Exists(itemName);
        }

        public IEnumerable<ProjectAllocationDto> GetAll(int pageSize=-1, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo);
        }

        public ProjectAllocationDto GetByID(int id)
        {
            return repository.GetByID(id);
        }

        public void Update(ProjectAllocationDto entity)
        {
            repository.Update(entity);
        }

        public int GetPercentageOfAllocation(int employeeID, int projectID)
        {
            return repository.GetPercentageOfAllocation(employeeID, projectID);
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }

        public IEnumerable<string> GetAllocatedProjectsByEmployeeID(int employeeID, int projectToExclude)
        {
            return repository.GetAllocatedProjectsByEmployeeID(employeeID, projectToExclude);
        }
    }
}
