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

        public SubPracticeDto GetSubPractice(int id)
        {
            return repository.GetByID(id);
        }

        public List<SubPracticeDto> GetSubPractices()
        {
            return repository.GetAll().ToList();
        }

        public List<SubPracticeDto> GetSubPractices(int practiceID)
        {
            return repository.GetAll(practiceID).ToList();
        }

        public void UpdateSubPractice(SubPracticeDto subPractice)
        {
            repository.Update(subPractice);
        }
    }
}
