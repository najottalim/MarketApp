using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SamidApp.Data.IRepositories;
using SamidApp.Domain.Configurations;
using SamidApp.Domain.Entities.Products;
using SamidApp.Service.DTOs;
using SamidApp.Service.Exceptions;
using SamidApp.Service.Helpers;
using SamidApp.Service.Interfaces;

namespace SamidApp.Service.Services;

public partial class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAttachmentService _attachmentService;
    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IAttachmentService attachmentService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _attachmentService = attachmentService;
    }
    
    public async Task<IEnumerable<Product>> GetAllAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null)
    {
        var pagedList = _unitOfWork.Products.GetAll(expression,  isTracking: false).ToPagedList(@params);

        return await pagedList.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllWithCategoriesAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null)
    {
        var pagedList = _unitOfWork.Products.GetAll(expression, "Category", false).ToPagedList(@params);

        return await pagedList.ToListAsync();
    }

    public async Task<Product> GetAsync(Expression<Func<Product, bool>> expression = null)
    {
        var product = await _unitOfWork.Products.GetAsync(expression);
        if (product is null)
            throw new MarketException(404, "Product not found");
        
        return product;
    }

    public async Task<Product> AddAsync(ProductForCreationDto dto)
    {
        // check for exist .. any code
        var attachment = await _attachmentService.UploadAsync(dto.File.OpenReadStream(), dto.File.FileName);
        
        // mapping
        var mappedProduct = _mapper.Map<Product>(dto);
        var product = await _unitOfWork.Products.AddAsync(mappedProduct);
        product.FileId = attachment.Id;
        
        await _unitOfWork.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateAsync(long id, ProductForCreationDto dto)
    {
        var product = await _unitOfWork.Products.GetAsync(p => p.Id == id);
        if (product is null)
            throw new MarketException(404, "Product not found");

        var mappedProduct = _mapper.Map(dto, product);
        var updatedProduct = await _unitOfWork.Products.UpdateAsync(mappedProduct);
        await _unitOfWork.SaveChangesAsync();

        return updatedProduct;
    }

    public async Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression)
    {
        var product = await _unitOfWork.Products.GetAsync(expression);
        
        product.Name = "asdasd";

        await _unitOfWork.Products.DeleteAsync(expression);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}