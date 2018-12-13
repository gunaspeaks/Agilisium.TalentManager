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
            SubPractice subPractice = CreateBusinessEntity(entity, true);
            Entities.Add(subPractice);
            DataContext.Entry(subPractice).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(SubPracticeDto entity)
        {
            SubPractice buzEntity = Entities.FirstOrDefault(e => e.SubPracticeID == entity.SubPracticeID);
            buzEntity.IsDeleted = true;
            Entities.Add(buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(string itemName, int id)
        {
            return Entities.Any(c => c.SubPracticeName.ToLower() == itemName.ToLower() &&
            c.SubPracticeID != id && c.IsDeleted == false);
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(c => c.SubPracticeName.ToLower() == itemName.ToLower() && c.IsDeleted == false);
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.SubPracticeID == id && c.IsDeleted == false);
        }

        public IEnumerable<SubPracticeDto> GetAll(int pageSize, int pageNo = -1)
        {
            return (from c in Entities
                    join p in DataContext.Practices on c.PracticeID equals p.PracticeID
                    orderby c.SubPracticeName
                    where c.IsDeleted == false
                    select new SubPracticeDto
                    {
                        PracticeID = c.PracticeID,
                        PracticeName = p.PracticeName,
                        SubPracticeID = c.SubPracticeID,
                        SubPracticeName = c.SubPracticeName,
                        ShortName = c.ShortName
                    });
        }

        public IEnumerable<SubPracticeDto> GetAllByPracticeID(int practiceID)
        {
            return (from c in Entities
                    join p in DataContext.Practices on c.PracticeID equals p.PracticeID into pr
                    from prd in pr.DefaultIfEmpty()
                    where c.PracticeID == practiceID && c.IsDeleted == false
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
                    where c.SubPracticeID == id && c.IsDeleted == false
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
            SubPractice buzEntity = Entities.FirstOrDefault(s => s.SubPracticeID == entity.SubPracticeID);
            MigrateEntity(entity, buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        private SubPractice CreateBusinessEntity(SubPracticeDto subPracticeDto, bool isNewEntity = false)
        {
            SubPractice subPractice = new SubPractice
            {
                PracticeID = subPracticeDto.PracticeID,
                SubPracticeName = subPracticeDto.SubPracticeName,
                ShortName = subPracticeDto.ShortName,
                SubPracticeID = subPracticeDto.SubPracticeID
            };

            subPractice.UpdateTimeStamp(subPracticeDto.LoggedInUserName, true);
            return subPractice;
        }

        private void MigrateEntity(SubPracticeDto sourceEntity, SubPractice targetEntity)
        {
            targetEntity.PracticeID = sourceEntity.PracticeID;
            targetEntity.SubPracticeName = sourceEntity.SubPracticeName;
            targetEntity.ShortName = sourceEntity.ShortName;
            targetEntity.SubPracticeID = sourceEntity.SubPracticeID;
            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }
    }

    public interface ISubPracticeRepository : IRepository<SubPracticeDto>
    {
        IEnumerable<SubPracticeDto> GetAllByPracticeID(int practiceID);

        bool Exists(string itemName, int id);
    }
}
