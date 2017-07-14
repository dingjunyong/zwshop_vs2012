using System.Collections.Generic;
using ZwShop.Data.Repository.Categories;

namespace ZwShop.Services.Categories
{
    public partial interface ICategoryService
    {
        bool IsCategoryAccessDenied(Category category);

        void CategoryDeleted(int id);

        List<Category> GetAllCategories(int? parentCategoryId=null, bool? showHidden=null);
                
        Category GetCategoryById(int categoryId);

        List<Category> GetBreadCrumb(int categoryId);

        void InsertCategory(Category category);

        void UpdateCategory(Category category);
    }
}
