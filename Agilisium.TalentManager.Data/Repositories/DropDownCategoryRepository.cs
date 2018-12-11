using Agilisium.TalentManager.Data.Abstract;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Data.Repositories
{
    public class DropDownCategoryRepository : RepositoryBase<DropDownCategory>, IDropDownCategoryRepository
    {
        public void Add(DropDownCategoryDto entity)
        {
            DropDownCategory category = ConvertToEntity(entity, true);
            Entities.Add(category);
            DataContext.Entry(category).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            DropDownCategory entity = Entities.FirstOrDefault(e => e.CategoryID == id);
            Entities.Remove(entity);
            DataContext.Entry(entity).State = EntityState.Deleted;
            DataContext.SaveChanges();
        }

        public bool Exists(string itemName, int id)
        {
            return Entities.Any(c => c.CategoryName.ToLower() == itemName.ToLower() && c.CategoryID != id);
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(c => c.CategoryName.ToLower() == itemName.ToLower());
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.CategoryID == id);
        }

        public IEnumerable<DropDownCategoryDto> GetAll()
        {
            return (from c in Entities
                    orderby c.CategoryName
                    select new DropDownCategoryDto
                    {
                        CategoryID = c.CategoryID,
                        CategoryName = c.CategoryName,
                        Description = c.Description,
                        ShortName = c.ShortName,
                        IsReserved = c.IsReserved
                    });
        }

        public DropDownCategoryDto GetByID(int id)
        {
            return (from c in Entities
                    where c.CategoryID == id
                    select new DropDownCategoryDto
                    {
                        CategoryID = c.CategoryID,
                        CategoryName = c.CategoryName,
                        Description = c.Description,
                        ShortName = c.ShortName,
                        IsReserved = c.IsReserved
                    }).FirstOrDefault();
        }

        public void Update(DropDownCategoryDto entity)
        {
            DropDownCategory category = ConvertToEntity(entity);
            Entities.Add(category);
            DataContext.Entry(category).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        private DropDownCategory ConvertToEntity(DropDownCategoryDto categoryDto, bool isNewEntity = false)
        {
            DropDownCategory category = new DropDownCategory
            {
                CategoryName = categoryDto.CategoryName,
                Description = categoryDto.Description,
                ShortName = categoryDto.ShortName
            };

            if (isNewEntity == false)
            {
                category.CategoryID = categoryDto.CategoryID;
            }

            return category;
        }
    }

    public interface IDropDownCategoryRepository : IRepository<DropDownCategoryDto>
    {
        bool Exists(string itemName, int id);
    }
}
