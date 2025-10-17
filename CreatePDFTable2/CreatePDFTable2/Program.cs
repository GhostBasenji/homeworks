// Программа "на лету" создает PDF-файл и записывает в этот файл "узкую" таблицу с малым числом колонок
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Diagnostics;

namespace Создать_PDF_Табл_2
{
    class Program
    {
        static void Main(string[] args)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            string[] Столицы = { "СТОЛИЦЫ", "Киев", "Москва", "Минск" };
            string[] Страны = { "ГОСУДАРСТВА", "Украина", "Россия", "Беларусь" };

            string fileName = "ОтчетТабл.pdf";

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);

                    page.Content().Column(column =>
                    {
                        column.Spacing(20);

                        // Текст до таблицы
                        column.Item()
                            .Text("Какой-либо текст до таблицы")
                            .FontColor(Colors.Blue.Medium)
                            .FontSize(12);

                        // Таблица с 2 колонками
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });

                            for (int i = 0; i < 4; i++)
                            {
                                bool isHeader = (i == 0);
                                var backgroundColor = isHeader ? Colors.Grey.Lighten3 :
                                                     (i % 2 == 0 ? Colors.White : Colors.Grey.Lighten4);

                                // Ячейка "Страны"
                                var страныCell = table.Cell()
                                    .Background(backgroundColor)
                                    .Border(1)
                                    .BorderColor(Colors.Blue.Medium)
                                    .Padding(8)
                                    .Text(Страны[i])
                                    .FontColor(Colors.Blue.Medium)
                                    .FontSize(10);

                                if (isHeader) страныCell.Bold();

                                // Ячейка "Столицы"
                                var столицыCell = table.Cell()
                                    .Background(backgroundColor)
                                    .Border(1)
                                    .BorderColor(Colors.Blue.Medium)
                                    .Padding(8)
                                    .Text(Столицы[i])
                                    .FontColor(Colors.Blue.Medium)
                                    .FontSize(10);

                                if (isHeader) столицыCell.Bold();
                            }
                        });

                        // Текст после таблицы
                        column.Item()
                            .Text("Некоторый текст после таблицы")
                            .FontColor(Colors.Blue.Medium)
                            .FontSize(12);
                    });
                });
            })
            .GeneratePdf(fileName);

            OpenPdfFile(fileName);
        }

        static void OpenPdfFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = fileName,
                        UseShellExecute = true
                    });
                    Console.WriteLine($"PDF файл успешно создан и открыт: {Path.GetFullPath(fileName)}");
                }
                else
                {
                    Console.WriteLine("Ошибка: PDF файл не был создан!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Файл создан: {Path.GetFullPath(fileName)}");
                Console.WriteLine($"Ошибка открытия: {ex.Message}");
            }
        }
    }
}