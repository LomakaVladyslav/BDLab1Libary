using Libary.Data;

namespace Libary.Services
{
    public class AutorDataPortServiceFactory : IDataPortServiceFactory<Autor>
    {
        private readonly DblibaryContext _context;
        public AutorDataPortServiceFactory(DblibaryContext context)
        {
            _context = context;
        }
        public IImportService<Autor> GetImportService(string contentType)
        {
            if (contentType is "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new AutorImportService(_context);
            }
            throw new NotImplementedException($"No import service implemented for movies with content type {contentType}");
        }
        public IExportService<Autor> GetExportService(string contentType)
        {
            if (contentType is "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new AutorExportService(_context);
            }
            throw new NotImplementedException($"No export service implemented for movies with content type {contentType}");
        }
    }

}
