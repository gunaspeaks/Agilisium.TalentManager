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

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public bool Exists(int empEntryID, int projectID)
        {
            return repository.Exists(empEntryID, projectID);
        }

        public bool Exists(int allocationID, int empEntryID, int projectID)
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

        public IEnumerable<ProjectAllocationDto> GetAll()
        {
            return repository.GetAll();
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
    }
}
