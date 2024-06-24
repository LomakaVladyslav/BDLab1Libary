using Libary.Data;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace Libary.Services
{
    public class AutorExportService : IExportService<Autor>
    {
        private static readonly IReadOnlyList<string> HeaderNames = new string[]
        {
            "Ім'я",
            "Псевдонім",
            "Регіон"
        };

        private readonly DblibaryContext _context;

        public AutorExportService(DblibaryContext context)
        {
            _context = context;
        }

        private static void WriteHeader(IXLWorksheet worksheet)
        {
            for (int columnIndex = 0; columnIndex < HeaderNames.Count; columnIndex++)
            {
                worksheet.Cell(1, columnIndex + 1).Value = HeaderNames[columnIndex];
            }
            worksheet.Row(1).Style.Font.Bold = true;
        }

        private void WriteAutor(IXLWorksheet worksheet, Autor autor, int rowIndex)
        {
            var columnIndex = 1;
            worksheet.Cell(rowIndex, columnIndex++).Value = autor.AutorName;
            worksheet.Cell(rowIndex, columnIndex++).Value = autor.Pseudonym;
            worksheet.Cell(rowIndex, columnIndex++).Value = autor.RegionId.ToString();
        }

        private void WriteAutors(IXLWorksheet worksheet, ICollection<Autor> autors)
        {
            WriteHeader(worksheet);
            int rowIndex = 2;
            foreach (var autor in autors)
            {
                WriteAutor(worksheet, autor, rowIndex);
                rowIndex++;
            }
        }

        public async Task WriteToAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (!stream.CanWrite)
            {
                throw new ArgumentException("Input stream is not writable");
            }

            var autors = await _context.Autors.ToListAsync(cancellationToken);
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Autors");
            WriteAutors(worksheet, autors);
            workbook.SaveAs(stream);
        }
    }
}
