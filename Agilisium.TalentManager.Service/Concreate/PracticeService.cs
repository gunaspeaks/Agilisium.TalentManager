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
    public class PracticeService : IPracticeService
    {
        private readonly IPracticeRepository repository;

        public PracticeService(IPracticeRepository repository)
        {
            this.repository = repository;
        }

        public void CreatePractice(PracticeDto practice)
        {
            repository.Add(practice);
        }

        public void DeletePractice(PracticeDto practice)
        {
            repository.Delete(practice);
        }

        public bool Exists(string practice)
        {
            return repository.Exists(practice);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public bool Exists(string practiceName, int id)
        {
            return repository.Exists(practiceName, id);
        }

        public IEnumerable<PracticeDto> GetPractices(int pageSize, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo);
        }

        public PracticeDto GetPractice(int id)
        {
            return repository.GetByID(id);
        }

        public void UpdatePractice(PracticeDto practice)
        {
            repository.Update(practice);
        }
    }
}
