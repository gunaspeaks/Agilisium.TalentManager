using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IDropDownSubCategoryService
    {
        bool Exists(string subCategoryName);

        bool Exists(int id);

        bool Exists(string subCategoryName, int id);

        List<DropDownSubCategoryDto> GetSubCategories();

        List<DropDownSubCategoryDto> GetSubCategories(int categoryID);

        DropDownSubCategoryDto GetSubCategory(int id);

        void CreateSubCategory(DropDownSubCategoryDto subCategory);

        void UpdateSubCategory(DropDownSubCategoryDto category);

        void DeleteSubCategory(int id);
    }
}
