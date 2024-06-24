using Libary.Data;
using ClosedXML.Excel;

namespace Libary.Services
{
    // Сервіс для імпорту авторів з Excel-файлу
    public class AutorImportService : IImportService<Autor>
    {
        private readonly DblibaryContext _context;

        // Конструктор класу, приймає контекст бази даних
        public AutorImportService(DblibaryContext context)
        {
            _context = context;
        }

        // Метод для імпорту даних з Excel-файлу
        public async Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
            // Перевірка, чи можливо читати дані з потоку
            if (!stream.CanRead)
            {
                throw new ArgumentException("Дані не можуть бути прочитані", nameof(stream));
            }

            // Використання бібліотеки ClosedXML для обробки Excel-файлу
            using (XLWorkbook workBook = new XLWorkbook(stream))
            {
                // Обробка кожного робочого листа у файлі Excel
                foreach (IXLWorksheet worksheet in workBook.Worksheets)
                {
                    // Обробка кожного рядка на робочому листі (пропускаючи перший рядок, який містить заголовок)
                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        await AddAutorAsync(row, cancellationToken); // Додавання автора
                    }
                }
            }
            await _context.SaveChangesAsync(cancellationToken); // Збереження змін у базі даних
        }

        // Метод для додавання автора до бази даних на основі даних з рядка Excel
        private async Task AddAutorAsync(IXLRow row, CancellationToken cancellationToken)
        {
            Autor autor = new Autor(); // Створення нового об'єкту автора
            autor.AutorName = row.Cell(1).Value.ToString(); // Ім'я автора
            autor.Pseudonym = row.Cell(2).Value.ToString(); // Псевдонім автора
            autor.RegionId = Convert.ToInt32(row.Cell(3).Value.ToString()); // ID регіону автора (якщо є)

            _context.Autors.Add(autor); // Додавання автора до контексту бази даних

            await _context.SaveChangesAsync(cancellationToken); // Збереження змін у базі даних
        }
    }
}
