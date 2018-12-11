using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IDropDownCategoryService
    {
        bool Exists(string categoryName);

        bool Exists(int id);

        bool Exists(string categoryName, int id);

        IEnumerable<DropDownCategoryDto> GetCategories();

        DropDownCategoryDto GetCategory(int id);

        void CreateCategory(DropDownCategoryDto category);

        void UpdateCategory(DropDownCategoryDto category);

        void DeleteCategory(int id);
    }
}
