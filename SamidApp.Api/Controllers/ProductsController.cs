using Microsoft.AspNetCore.Mvc;
using SamidApp.Domain.Configurations;
using SamidApp.Domain.Entities.Products;
using SamidApp.Service.DTOs;
using SamidApp.Service.Interfaces;

namespace SamidApp.Api.Controllers;

public class ProductsController : BaseController
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Product CRUD
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await _productService.GetAllWithCategoriesAsync(@params));

    [HttpGet("{Id}")]
    public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
        => Ok(await _productService.GetAsync(p => p.Id == id));

    [HttpPost]
    public async Task<ActionResult<Product>> AddAsync([FromForm]ProductForCreationDto dto)
        => Ok(await _productService.AddAsync(dto));

    [HttpPut("{Id}")]
    public async Task<ActionResult<Product>> UpdateAsync([FromRoute(Name = "Id")] long id, ProductForCreationDto dto)
        => Ok(await _productService.UpdateAsync(id, dto));

    [HttpDelete("{Id}")]
    public async Task<ActionResult<bool>> DeleteAsync([FromRoute(Name = "Id")] long id)
        => Ok(await _productService.DeleteAsync(p => p.Id == id));

    /// <summary>
    /// CRUD of Category of product
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpPost("Categories")]
    public async Task<ActionResult<ProductCategory>> AddCategoryAsync([FromBody] string name)
        => Ok(await _productService.AddCategoryAsync(name));

    [HttpGet("Categories")]
    public async Task<IActionResult> GetAllCategoryAsync([FromQuery]PaginationParams @params)
        => Ok(await _productService.GetAllCategoryWithProductsAsync(@params));

    [HttpGet("Categories/{Id}")]
    public async Task<ActionResult<ProductCategory>> GetCategoryAsync([FromRoute(Name = "Id")] long id)
        => Ok(await _productService.GetCategoryAsync(p => p.Id == id));

    [HttpPut("Categories/{Id}")]
    public async Task<ActionResult<ProductCategory>> UpdateCategoryAsync([FromRoute(Name = "Id")] long id, string name)
        => Ok(await _productService.UpdateCategoryAsync(id, name));

    [HttpDelete("Categories/{Id}")]
    public async Task<ActionResult<bool>> DeleteCategoryAsync([FromRoute(Name = "Id")] long id)
        => Ok(await _productService.DeleteCategoryAsync(p => p.Id == id));

}