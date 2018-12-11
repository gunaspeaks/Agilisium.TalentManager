using Agilisium.TalentManager.Data.Abstract;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Data.Repositories
{
    public class DropDownSubCategoryRepository : RepositoryBase<DropDownSubCategory>, IDropDownSubCategoryRepository
    {
        public void Add(DropDownSubCategoryDto entity)
        {
            DropDownSubCategory subCategory = ConvertToEntity(entity, true);
            Entities.Add(subCategory);
            DataContext.Entry(subCategory).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            DropDownSubCategory entity = Entities.FirstOrDefault(e => e.CategoryID == id);
            Entities.Remove(entity);
            DataContext.Entry(entity).State = EntityState.Deleted;
            DataContext.SaveChanges();
        }

        public bool Exists(string itemName, int id)
        {
            return Entities.Any(c => c.SubCategoryName.ToLower() == itemName.ToLower() && c.SubCategoryID != id);
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(c => c.SubCategoryName.ToLower() == itemName.ToLower());
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.SubCategoryID == id);

        }

        public IEnumerable<DropDownSubCategoryDto> GetAll()
        {
            return (from s in Entities
                    join c in DataContext.DropDownCategories on s.CategoryID equals c.CategoryID into ce
                    from cd in ce.DefaultIfEmpty()
                    orderby s.SubCategoryName
                    select new DropDownSubCategoryDto
                    {
                        SubCategoryID = s.SubCategoryID,
                        SubCategoryName = s.SubCategoryName,
                        CategoryID = s.CategoryID,
                        Description = s.Description,
                        ShortName = s.ShortName,
                        CategoryName = cd.CategoryName,
                        IsReserved = cd.IsReserved
                    });
        }

        public DropDownSubCategoryDto GetByID(int id)
        {
            return (from s in Entities
                    join c in DataContext.DropDownCategories on s.CategoryID equals c.CategoryID
                    where s.SubCategoryID == id
                    select new DropDownSubCategoryDto
                    {
                        SubCategoryID = s.SubCategoryID,
                        SubCategoryName = s.SubCategoryName,
                        CategoryID = s.CategoryID,
                        Description = s.Description,
                        ShortName = s.ShortName,
                        CategoryName = c.CategoryName
                    }).FirstOrDefault();
        }

        public IEnumerable<DropDownSubCategoryDto> GetSubCategories(int categoryID)
        {
            return (from s in Entities
                    join c in DataContext.DropDownCategories on s.CategoryID equals c.CategoryID into ce
                    from cd in ce.DefaultIfEmpty()
                    where s.CategoryID == categoryID
                    select new DropDownSubCategoryDto
                    {
                        SubCategoryID = s.SubCategoryID,
                        SubCategoryName = s.SubCategoryName,
                        CategoryID = s.CategoryID,
                        Description = s.Description,
                        ShortName = s.ShortName,
                        CategoryName = cd.CategoryName,
                        IsReserved = cd.IsReserved
                    });
        }

        public void Update(DropDownSubCategoryDto entity)
        {
            DropDownSubCategory subCategory = ConvertToEntity(entity, true);
            Entities.Add(subCategory);
            DataContext.Entry(subCategory).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        private DropDownSubCategory ConvertToEntity(DropDownSubCategoryDto subCategoryDto, bool isNewEntity = false)
        {
            DropDownSubCategory category = new DropDownSubCategory
            {
                CategoryID = subCategoryDto.CategoryID,
                SubCategoryID = subCategoryDto.SubCategoryID,
                SubCategoryName = subCategoryDto.SubCategoryName,
                Description = subCategoryDto.Description,
                ShortName = subCategoryDto.ShortName
            };

            if (isNewEntity == false)
            {
                category.SubCategoryID = subCategoryDto.SubCategoryID;
            }

            return category;
        }

    }

    public interface IDropDownSubCategoryRepository : IRepository<DropDownSubCategoryDto>
    {
        IEnumerable<DropDownSubCategoryDto> GetSubCategories(int categoryID);

        bool Exists(string itemName, int id);
    }
}
