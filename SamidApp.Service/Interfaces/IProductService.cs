using System.Linq.Expressions;
using SamidApp.Domain.Configurations;
using SamidApp.Domain.Entities.Products;
using SamidApp.Service.DTOs;

namespace SamidApp.Service.Interfaces;

public partial interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null);
    Task<IEnumerable<Product>> GetAllWithCategoriesAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null);
    Task<Product> GetAsync(Expression<Func<Product, bool>> expression = null);
    Task<Product> AddAsync(ProductForCreationDto dto);
    Task<Product> UpdateAsync(long id, ProductForCreationDto dto);
    Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression);
}