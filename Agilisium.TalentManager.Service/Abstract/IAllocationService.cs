﻿using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IAllocationService
    {
        void Add(ProjectAllocationDto entity);

        void Delete(ProjectAllocationDto entity);

        bool Exists(int empEntryID, int projectID);

        bool Exists(int allocationID, int empEntryID, int projectID);

        bool Exists(int id);

        bool Exists(string itemName);

        IEnumerable<ProjectAllocationDto> GetAll(int pageNo = -1);

        ProjectAllocationDto GetByID(int id);

        void Update(ProjectAllocationDto entity);

        int GetPercentageOfAllocation(int employeeID, int projectID);
    }
}
