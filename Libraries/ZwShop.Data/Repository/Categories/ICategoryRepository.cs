using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZwShop.Data.Repository.Categories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();

        int InsertCategory(Category objRecord);

        int UpdateCategory(Category objRecord);

        int DeleteCategory(int id);
    }
}
