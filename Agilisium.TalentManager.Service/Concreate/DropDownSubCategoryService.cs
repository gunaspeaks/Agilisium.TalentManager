using Agilisium.TalentManager.Data.Repositories;
using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class DropDownSubCategoryService : IDropDownSubCategoryService
    {
        private readonly IDropDownSubCategoryRepository repository;

        public DropDownSubCategoryService(IDropDownSubCategoryRepository repository)
        {
            this.repository = repository;
        }

        public void CreateSubCategory(DropDownSubCategoryDto subCategory)
        {
            repository.Add(subCategory);
        }

        public bool Exists(string subCategoryName)
        {
            return repository.Exists(subCategoryName);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public bool Exists(string subCategoryName, int id)
        {
            return repository.Exists(subCategoryName, id);
        }

        public List<DropDownSubCategoryDto> GetSubCategories(int pageSize=0, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo).ToList();
        }

        public List<DropDownSubCategoryDto> GetSubCategories(int categoryID)
        {
            return repository.GetSubCategories(categoryID).ToList();
        }

        public DropDownSubCategoryDto GetSubCategory(int id)
        {
            return repository.GetByID(id);
        }

        public void UpdateSubCategory(DropDownSubCategoryDto category)
        {
            repository.Update(category);
        }

        public void DeleteSubCategory(DropDownSubCategoryDto category)
        {
            repository.Delete(category);
        }
    }
}
