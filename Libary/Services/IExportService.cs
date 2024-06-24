using Libary.Data;

namespace Libary.Services
{
    public interface IExportService<TEntity>
    where TEntity : Entity
    {
        Task WriteToAsync(Stream stream, CancellationToken cancellationToken);
    }
}