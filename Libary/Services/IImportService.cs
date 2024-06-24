using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Libary.Data;

namespace Libary.Services
{
    public interface IImportService<TEntity> where TEntity : Entity
    {
        Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken);
    }
}
