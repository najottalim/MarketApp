using System.Linq.Expressions;
using SamidApp.Domain.Configurations;
using SamidApp.Domain.Entities.Users;
using SamidApp.Service.DTOs.Users;

namespace SamidApp.Service.Interfaces;

public interface IUserService
{
    Task<User> AddAsync(UserForCreationDto dto);
    Task<User> UpdateAsync(long id, UserForCreationDto dto);
    Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
    Task<User> GetAsync(Expression<Func<User, bool>> expression);
    Task<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null);
}