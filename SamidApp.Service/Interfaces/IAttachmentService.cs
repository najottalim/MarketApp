using System.Linq.Expressions;
using SamidApp.Domain.Configurations;
using SamidApp.Domain.Entities.Commons;
using SamidApp.Domain.Entities.Products;
using SamidApp.Service.DTOs;

namespace SamidApp.Service.Interfaces;

public interface IAttachmentService
{
    Task<Attachment> UploadAsync(Stream stream, string fileName);
    Task<Attachment> UpdateAsync(long id, Stream stream);
    Task<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression);
}