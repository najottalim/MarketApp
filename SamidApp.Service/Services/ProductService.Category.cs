using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SamidApp.Data.IRepositories;
using SamidApp.Domain.Configurations;
using SamidApp.Domain.Entities.Products;
using SamidApp.Service.Exceptions;
using SamidApp.Service.Helpers;

namespace SamidApp.Service.Services;

public partial class ProductService
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    public ProductService(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }
    
    public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync(PaginationParams @params, Expression<Func<ProductCategory, bool>> expression = null)
    {
        var pagedList = _productCategoryRepository.GetAll(expression, isTracking: false).ToPagedList(@params);

        return await pagedList.ToListAsync();
    }

    public async Task<IEnumerable<ProductCategory>> GetAllCategoryWithProductsAsync(PaginationParams @params, Expression<Func<ProductCategory, bool>> expression = null)
    {
        var pagedList = _productCategoryRepository.GetAll(expression, "Products", false).ToPagedList(@params);

        return await pagedList.ToListAsync();
    }

    public async Task<ProductCategory> GetCategoryAsync(Expression<Func<ProductCategory, bool>> expression = null)
    {
        var category = await _productCategoryRepository.GetAsync(expression);
        if (category is null)
            throw new MarketException(404, "Product category not found");

        return category;
    }

    public async Task<ProductCategory> AddCategoryAsync(string category)
    {
        var newCategory = new ProductCategory()
        {
            Name = category
        };
        var result = await _productCategoryRepository.AddAsync(newCategory);

        await _productCategoryRepository.SaveChangesAsync();

        return result;
    }

    public async Task<ProductCategory> UpdateCategoryAsync(long id, string dto)
    {
        var existCategory = await _productCategoryRepository.GetAsync(p => p.Id == id);
        if (existCategory is null)
            throw new MarketException(404, "Product category not found");
        
        existCategory.Name = dto;
        existCategory.UpdatedAt = DateTime.UtcNow;
            
        await _productCategoryRepository.SaveChangesAsync();

        return existCategory;
    }

    public async Task<bool> DeleteCategoryAsync(Expression<Func<ProductCategory, bool>> expression)
    {
        var existCategory = await _productCategoryRepository.GetAsync(expression);
        if (existCategory is null)
            throw new MarketException(404, "Product category not found");
        
        await _productCategoryRepository.DeleteAsync(expression);
        await _productCategoryRepository.SaveChangesAsync();

        return true;
    }
}