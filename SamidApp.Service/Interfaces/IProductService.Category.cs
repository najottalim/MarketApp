using System.Linq.Expressions;
using SamidApp.Domain.Configurations;
using SamidApp.Domain.Entities.Products;
using SamidApp.Service.DTOs;

namespace SamidApp.Service.Interfaces;

public partial interface IProductService
{
    Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync(PaginationParams @params, Expression<Func<ProductCategory, bool>> expression = null);
    Task<IEnumerable<ProductCategory>> GetAllCategoryWithProductsAsync(PaginationParams @params, Expression<Func<ProductCategory, bool>> expression = null);
    Task<ProductCategory> GetCategoryAsync(Expression<Func<ProductCategory, bool>> expression = null);
    Task<ProductCategory> AddCategoryAsync(string category);
    Task<ProductCategory> UpdateCategoryAsync(long id, string dto);
    Task<bool> DeleteCategoryAsync(Expression<Func<ProductCategory, bool>> expression);
}