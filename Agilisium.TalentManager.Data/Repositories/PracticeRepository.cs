using Agilisium.TalentManager.Data.Abstract;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Data.Repositories
{
    public class PracticeRepository : RepositoryBase<Practice>, IPracticeRepository
    {
        public void Add(PracticeDto entity)
        {
            Practice practice = ConvertToEntity(entity, true);
            Entities.Add(practice);
            DataContext.Entry(practice).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Practice entity = Entities.FirstOrDefault(e => e.PracticeID == id);
            Entities.Remove(entity);
            DataContext.Entry(entity).State = EntityState.Deleted;
            DataContext.SaveChanges();
        }

        public bool Exists(string itemName, int id)
        {
            return Entities.Any(c => c.PracticeName.ToLower() == itemName.ToLower() && c.PracticeID != id);
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(c => c.PracticeName.ToLower() == itemName.ToLower());
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.PracticeID == id);
        }

        public IEnumerable<PracticeDto> GetAll()
        {
            return (from c in Entities
                    orderby c.PracticeName
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
                    where c.PracticeID == id
                    select new PracticeDto
                    {
                        PracticeID = c.PracticeID,
                        PracticeName = c.PracticeName,
                        ShortName = c.ShortName
                    }).FirstOrDefault();
        }

        public void Update(PracticeDto entity)
        {
            Practice practice = ConvertToEntity(entity);
            Entities.Add(practice);
            DataContext.Entry(practice).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        private Practice ConvertToEntity(PracticeDto categoryDto, bool isNewEntity = false)
        {
            Practice practice = new Practice
            {
                PracticeName = categoryDto.PracticeName,
                ShortName = categoryDto.ShortName
            };

            if (isNewEntity == false)
            {
                practice.PracticeID = categoryDto.PracticeID;
            }

            return practice;
        }
    }

    public interface IPracticeRepository : IRepository<PracticeDto>
    {
        bool Exists(string itemName, int id);
    }
}
