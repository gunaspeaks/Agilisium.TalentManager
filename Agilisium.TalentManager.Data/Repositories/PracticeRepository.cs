using Agilisium.TalentManager.Data.Abstract;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Data.Repositories
{
    public class PracticeRepository : RepositoryBase<Practice>, IPracticeRepository
    {
        public void Add(PracticeDto entity)
        {
            Practice practice = CreateBusinessEntity(entity, true);
            Entities.Add(practice);
            DataContext.Entry(practice).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(PracticeDto entity)
        {
            Practice buzEntity = Entities.FirstOrDefault(e => e.PracticeID == entity.PracticeID);
            buzEntity.IsDeleted = true;
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(string itemName, int id)
        {
            return Entities.Any(c => c.PracticeName.ToLower() == itemName.ToLower() &&
            c.PracticeID != id && c.IsDeleted == false);
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(c => c.PracticeName.ToLower() == itemName.ToLower() && c.IsDeleted == false);
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.PracticeID == id && c.IsDeleted == false);
        }

        public IEnumerable<PracticeDto> GetAll(int pageSize, int pageNo = -1)
        {
            return (from c in Entities
                    orderby c.PracticeName
                    where c.IsDeleted == false
                    select new PracticeDto
                    {
                        PracticeID = c.PracticeID,
                        PracticeName = c.PracticeName,
                        ShortName = c.ShortName,
                        IsReserved = c.IsReserved
                    });
        }

        public PracticeDto GetByID(int id)
        {
            return (from c in Entities
                    where c.PracticeID == id && c.IsDeleted == false
                    select new PracticeDto
                    {
                        PracticeID = c.PracticeID,
                        PracticeName = c.PracticeName,
                        ShortName = c.ShortName
                    }).FirstOrDefault();
        }

        public void Update(PracticeDto entity)
        {
            Practice buzEntity = Entities.FirstOrDefault(e => e.PracticeID == entity.PracticeID);
            MigrateEntity(entity, buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        private Practice CreateBusinessEntity(PracticeDto categoryDto, bool isNewEntity = false)
        {
            Practice practice = new Practice
            {
                PracticeName = categoryDto.PracticeName,
                ShortName = categoryDto.ShortName,
                PracticeID = categoryDto.PracticeID
            };

            practice.UpdateTimeStamp(categoryDto.LoggedInUserName, true);

            return practice;
        }

        private void MigrateEntity(PracticeDto sourceEntity, Practice targetEntity)
        {
            targetEntity.PracticeName = sourceEntity.PracticeName;
            targetEntity.ShortName = sourceEntity.ShortName;
            targetEntity.PracticeID = sourceEntity.PracticeID;
            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }
    }

    public interface IPracticeRepository : IRepository<PracticeDto>
    {
        bool Exists(string itemName, int id);
    }
}
