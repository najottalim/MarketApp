using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SamidApp.Data.IRepositories;
using SamidApp.Domain.Configurations;
using SamidApp.Domain.Entities.Products;
using SamidApp.Service.DTOs;
using SamidApp.Service.Helpers;
using SamidApp.Service.Interfaces;

namespace SamidApp.Service.Services;

public partial class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public ProductService(IProductRepository productRepository, IMapper mapper, IProductCategoryRepository productCategoryRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _productCategoryRepository = productCategoryRepository;
    }
    
    public async Task<IEnumerable<Product>> GetAllAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null)
    {
        var pagedList = _productRepository.GetAll(expression,  isTracking: false).ToPagedList(@params);

        return await pagedList.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllWithCategoriesAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null)
    {
        var pagedList = _productRepository.GetAll(expression, "Category", false).ToPagedList(@params);

        return await pagedList.ToListAsync();
    }

    public async Task<Product> GetAsync(Expression<Func<Product, bool>> expression = null)
    {
        return await _productRepository.GetAsync(expression);
    }

    public async Task<Product> AddAsync(ProductForCreationDto dto)
    {
        // check for exist .. any code
        // ...
        
        // mapping
        var mappedProduct = _mapper.Map<Product>(dto);
        var product = await _productRepository.AddAsync(mappedProduct);

        await _productRepository.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateAsync(long id, ProductForCreationDto dto)
    {
        var product = await _productRepository.GetAsync(p => p.Id == id);
        if (product is null)
        {
            throw new Exception("Product not found");
        }

        var mappedProduct = _mapper.Map(dto, product);
        var updatedProduct = await _productRepository.UpdateAsync(mappedProduct);
        await _productRepository.SaveChangesAsync();

        return updatedProduct;
    }

    public async Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression)
    {
        var product = await _productRepository.GetAsync(expression);
        if (product is null)
        {
            return false;
        }

        await _productRepository.DeleteAsync(expression);
        await _productRepository.SaveChangesAsync();

        return true;
    }
}