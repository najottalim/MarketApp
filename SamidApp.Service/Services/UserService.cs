using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SamidApp.Data.IRepositories;
using SamidApp.Domain.Configurations;
using SamidApp.Domain.Entities.Users;
using SamidApp.Domain.Enums;
using SamidApp.Service.DTOs.Users;
using SamidApp.Service.Exceptions;
using SamidApp.Service.Helpers;
using SamidApp.Service.Interfaces;

namespace SamidApp.Service.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<User> AddAsync(UserForCreationDto dto)
    {
        // check for exist
        var user = await unitOfWork.Users.GetAsync(p => 
            p.Login.Equals(dto.Login) && p.State != ItemState.Deleted);
        if (user is not null)
            throw new MarketException(400, "User already exist");
        
        // map entity
        var newUser = mapper.Map<User>(dto);

        newUser = await unitOfWork.Users.AddAsync(newUser);
        await unitOfWork.SaveChangesAsync();

        return newUser;
    }

    public async Task<User> UpdateAsync(long id, UserForCreationDto dto)
    {
        // check for exist
        var user = await unitOfWork.Users.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);
        if (user is null)
            throw new MarketException(404, "User not found");
        
        // check for exist already
        var existUser = await unitOfWork.Users.GetAsync(p =>
            p.Login.Equals(dto.Login) && p.State != ItemState.Deleted);
        if (existUser is not null)
            throw new MarketException(400, "This login already exist");
        
        var mappedUser = mapper.Map(dto, user);

        mappedUser.State = ItemState.Updated;
        mappedUser.UpdatedAt = DateTime.UtcNow;

        var updatedUser = await unitOfWork.Users.UpdateAsync(mappedUser);

        await unitOfWork.SaveChangesAsync();

        return updatedUser;
    }

    public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
    {
        // check for exist
        var user = await unitOfWork.Users.GetAsync(expression);
        if (user is null)
            throw new MarketException(404, "User not found");

        user.State = ItemState.Deleted;
        user.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
    {
        var user = await unitOfWork.Users.GetAsync(expression);
        if (user is null)
            throw new MarketException(404, "User not found");

        return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
    {
        var users = unitOfWork.Users.GetAll(expression, isTracking: false);

        return await users.ToPagedList(@params).ToListAsync();
    }
}