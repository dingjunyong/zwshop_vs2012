using System;
using System.Collections.Generic;
using System.Linq;
using ZwShop.Services.Caching;
using ZwShop.Services.Configuration.Settings;
using ZwShop.Data;
using ZwShop.Services.Infrastructure;
using ZwShop.Common.Utils;
using ZwShop.Data.Repository.Categories;

namespace ZwShop.Services.Categories
{
    public partial class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }
        public bool IsCategoryAccessDenied(Category category)
        {
            return true;
        }
        public void CategoryDeleted(int id)
        {
            _categoryRepository.DeleteCategory(id);
        }

        public List<Category> GetAllCategories(int? parentCategoryId=null, bool? showHidden=null)
        {
            if (parentCategoryId.HasValue)
            {

            }
            else {
 
            }

            bool showHidden = ShopContext.Current.IsAdmin;
            return GetAllCategories(showHidden);
        }
        
                
        public Category GetCategoryById(int id)
        {
            if (id == 0)
                return null;
            return _categoryRepository.GetCategoryById(id);
        }


        public List<Category> GetBreadCrumb(int categoryId)
        {
            var breadCrumb = new List<Category>();
            var category = GetCategoryById(categoryId);

            while (category != null &&
                !category.Deleted &&
                category.Published)
            {
                breadCrumb.Add(category);
                category = category.ParentCategory;
            }
            breadCrumb.Reverse();
            return breadCrumb;
        }
        public void InsertCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            category.Name = CommonHelper.EnsureNotNull(category.Name);
            category.Name = CommonHelper.EnsureMaximumLength(category.Name, 400);
            category.Description = CommonHelper.EnsureNotNull(category.Description);
            _categoryRepository.InsertCategory(category);
            //if (this.CategoriesCacheEnabled || this.MappingsCacheEnabled)
            //{
            //    _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            //    _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);
            //}
        }

        public void UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            category.Name = CommonHelper.EnsureNotNull(category.Name);
            category.Name = CommonHelper.EnsureMaximumLength(category.Name, 400);
            category.Description = CommonHelper.EnsureNotNull(category.Description);

            var parentCategory = GetCategoryById(category.ParentCategoryId);
            while (parentCategory != null)
            {
                if (category.Id == parentCategory.Id)
                {
                    category.ParentCategoryId = 0;
                    break;
                }
                parentCategory = GetCategoryById(parentCategory.ParentCategoryId);
            }
            _categoryRepository.UpdateCategory(category);

            //if (this.CategoriesCacheEnabled || this.MappingsCacheEnabled)
            //{
            //    _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            //    _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);
            //}
        }
    }
}
