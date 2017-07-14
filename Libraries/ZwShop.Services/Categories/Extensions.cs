using System;
using System.Collections.Generic;
using System.Linq;
using ZwShop.Data.Repository.Categories;
using ZwShop.Services.Security;

namespace ZwShop.Services.Categories
{
    public static class Extensions
    {
        public static List<Category> SortCategoriesForTree(this List<Category> source, int parentId)
        {
            var result = new List<Category>();

            var temp = source.FindAll(c => c.ParentCategoryId == parentId);
            foreach (var cat in temp)
            {
                result.Add(cat);
                result.AddRange(SortCategoriesForTree(source, cat.Id));
            }
            return result;
        }
    }
}
