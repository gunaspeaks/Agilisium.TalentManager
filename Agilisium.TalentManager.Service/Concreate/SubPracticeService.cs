using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class SubPracticeService : ISubPracticeService
    {
        private readonly ISubPracticeRepository repository;

        public SubPracticeService(ISubPracticeRepository repository)
        {
            this.repository = repository;
        }

        public void CreateSubPractice(SubPracticeDto subPractice)
        {
            repository.Add(subPractice);
        }

        public void DeleteSubPractice(SubPracticeDto subPractice)
        {
            repository.Delete(subPractice);
        }

        public bool Exists(string subPracticeName)
        {
            return repository.Exists(subPracticeName);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public bool Exists(string subPracticeName, int id)
        {
            return repository.Exists(subPracticeName, id);
        }

        public SubPracticeDto GetByID(int subPracticeID)
        {
            return repository.GetByID(subPracticeID);
        }

        public IEnumerable<SubPracticeDto> GetSubPractices(int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo);
        }

        public IEnumerable<SubPracticeDto> GetAllByPracticeID(int practiceID, int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAllByPracticeID(practiceID, pageSize, pageNo);
        }

        public void UpdateSubPractice(SubPracticeDto subPractice)
        {
            repository.Update(subPractice);
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }

        public int TotalRecordsCountByPracticeID(int practiceID)
        {
            return repository.TotalRecordsCountByPracticeID(practiceID);
        }

        public bool CanBeDeleted(int id)
        {
            return repository.CanBeDeleted(id);
        }
    }
}
