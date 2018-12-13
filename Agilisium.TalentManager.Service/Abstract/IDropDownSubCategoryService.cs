using Agilisium.TalentManager.Dto;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IDropDownSubCategoryService
    {
        bool Exists(string subCategoryName);

        bool Exists(int id);

        bool Exists(string subCategoryName, int id);

        List<DropDownSubCategoryDto> GetSubCategories(int pageSize = 0, int pageNo = -1);

        List<DropDownSubCategoryDto> GetSubCategories(int categoryID);

        DropDownSubCategoryDto GetSubCategory(int id);

        void CreateSubCategory(DropDownSubCategoryDto subCategory);

        void UpdateSubCategory(DropDownSubCategoryDto category);

        void DeleteSubCategory(DropDownSubCategoryDto category);
    }
}
