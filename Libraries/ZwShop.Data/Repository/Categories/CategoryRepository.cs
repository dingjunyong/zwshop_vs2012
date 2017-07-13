using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Dapper;

namespace ZwShop.Data.Repository.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopObjectContext _context;
        public CategoryRepository(ShopObjectContext context)
        {
            this._context = context;
        }
        public IEnumerable<Category> GetCategories()
        {
            using (var conn = _context.OpenConnection())
            {
                var result = conn.Query<Category>(
                   "category_get_all",
                   commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public int InsertCategory(Category objRecord)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@Name", objRecord.Name);
                param.Add("@Description", objRecord.Description);
                param.Add("@ParentCategoryId", objRecord.ParentCategoryId);
                param.Add("@PictureId", objRecord.PictureId);
                param.Add("@Published", objRecord.Published);
                param.Add("@Deleted", objRecord.Deleted);
                param.Add("@DisplayOrder", objRecord.DisplayOrder);
                param.Add("@CreatedOn", objRecord.CreatedOn);
                param.Add("@UpdatedOn", objRecord.UpdatedOn);
                var result = conn.Execute(
                   "category_add",
                   param: param,
                   commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public int UpdateCategory(Category objRecord)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@Id", objRecord.Id);
                param.Add("@Name", objRecord.Name);
                param.Add("@Description", objRecord.Description);
                param.Add("@ParentCategoryId", objRecord.ParentCategoryId);
                param.Add("@PictureId", objRecord.PictureId);
                param.Add("@Published", objRecord.Published);
                param.Add("@Deleted", objRecord.Deleted);
                param.Add("@DisplayOrder", objRecord.DisplayOrder);
                param.Add("@CreatedOn", objRecord.CreatedOn);
                param.Add("@UpdatedOn", objRecord.UpdatedOn);
                var result = conn.Execute(
                   "category_update",
                   param: param,
                   commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public int DeleteCategory(int id)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                var result = conn.Execute(
                   "Category_delete_by_id",
                   param: param,
                   commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
