using Agilisium.TalentManager.Data.Abstract;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Data.Repositories
{
    public class SubPracticeRepository : RepositoryBase<SubPractice>, ISubPracticeRepository
    {
        public void Add(SubPracticeDto entity)
        {
            SubPractice subPractice = ConvertToEntity(entity, true);
            Entities.Add(subPractice);
            DataContext.Entry(subPractice).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            SubPractice entity = Entities.FirstOrDefault(e => e.SubPracticeID == id);
            Entities.Remove(entity);
            DataContext.Entry(entity).State = EntityState.Deleted;
            DataContext.SaveChanges();
        }

        public bool Exists(string itemName, int id)
        {
            return Entities.Any(c => c.SubPracticeName.ToLower() == itemName.ToLower() && c.SubPracticeID != id);
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(c => c.SubPracticeName.ToLower() == itemName.ToLower());
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.SubPracticeID == id);
        }

        public IEnumerable<SubPracticeDto> GetAll()
        {
            return (from c in Entities
                    join p in DataContext.Practices on c.PracticeID equals p.PracticeID
                    orderby c.SubPracticeName
                    select new SubPracticeDto
                    {
                        PracticeID = c.PracticeID,
                        PracticeName = p.PracticeName,
                        SubPracticeID = c.SubPracticeID,
                        SubPracticeName = c.SubPracticeName,
                        ShortName = c.ShortName
                    });
        }

        public IEnumerable<SubPracticeDto> GetAll(int practiceID)
        {
            return (from c in Entities
                    join p in DataContext.Practices on c.PracticeID equals p.PracticeID into pr
                    from prd in pr.DefaultIfEmpty()
                    where c.PracticeID == practiceID
                    orderby c.SubPracticeName
                    select new SubPracticeDto
                    {
                        PracticeID = c.PracticeID,
                        PracticeName = prd.PracticeName,
                        SubPracticeID = c.SubPracticeID,
                        SubPracticeName = c.SubPracticeName,
                        ShortName = c.ShortName
                    });
        }

        public SubPracticeDto GetByID(int id)
        {
            return (from c in Entities
                    where c.SubPracticeID == id
                    select new SubPracticeDto
                    {
                        PracticeID = c.PracticeID,
                        SubPracticeID = c.SubPracticeID,
                        SubPracticeName = c.SubPracticeName,
                        ShortName = c.ShortName
                    }).FirstOrDefault();
        }

        public void Update(SubPracticeDto entity)
        {
            SubPractice SubPractice = ConvertToEntity(entity);
            Entities.Add(SubPractice);
            DataContext.Entry(SubPractice).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        private SubPractice ConvertToEntity(SubPracticeDto categoryDto, bool isNewEntity = false)
        {
            SubPractice subPractice = new SubPractice
            {
                PracticeID = categoryDto.PracticeID,
                SubPracticeName = categoryDto.SubPracticeName,
                ShortName = categoryDto.ShortName
            };

            if (isNewEntity == false)
            {
                subPractice.SubPracticeID = categoryDto.SubPracticeID;
            }

            return subPractice;
        }
    }

    public interface ISubPracticeRepository : IRepository<SubPracticeDto>
    {
        IEnumerable<SubPracticeDto> GetAll(int practiceID);

        bool Exists(string itemName, int id);
    }
}
