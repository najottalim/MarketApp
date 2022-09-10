using System.Linq.Expressions;
using SamidApp.Data.IRepositories;
using SamidApp.Domain.Configurations;
using SamidApp.Domain.Entities.Commons;
using SamidApp.Service.Helpers;
using SamidApp.Service.Interfaces;

namespace SamidApp.Service.Services;

public class AttachmentService : IAttachmentService
{
    private readonly IUnitOfWork unitOfWork;

    public AttachmentService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Attachment> UploadAsync(Stream stream, string fileName)
    {
        // store to wwwroot
        fileName = Guid.NewGuid().ToString("N") + "-" + fileName;
        string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

        FileStream fileStream = File.Create(filePath);
        await stream.CopyToAsync(fileStream);

        await fileStream.FlushAsync();
        fileStream.Close();

        // store to database
        var attachment = await unitOfWork.Attachments.AddAsync(new Attachment()
        {
            Name = Path.GetFileName(filePath),
            Path = Path.Combine(EnvironmentHelper.FilePath, Path.GetFileName(filePath)),
            CreatedAt = DateTime.UtcNow
        });

        await unitOfWork.SaveChangesAsync();

        return attachment;
    }

    public Task<Attachment> UpdateAsync(long id, Stream stream)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression)
    {
        throw new NotImplementedException();
    }
}