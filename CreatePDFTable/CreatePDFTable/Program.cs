using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Diagnostics;

namespace CreatePDFTable1
{
    class Program
    {
        static void Main(string[] args)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var data = new[]
            {
                new[] { "N п/п", "ГОСУДАРСТВА", "СТОЛИЦЫ", "НАСЕЛЕНИЕ" },
                new[] { "1", "Украина", "Киев", "2 760 000" },
                new[] { "2", "Россия", "Москва", "10 380 000" },
                new[] { "3", "Беларусь", "Минск", "1 740 000" }
            };

            string fileName = "ОтчетТабл1.pdf";

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Header().Text("Таблица государств").Bold().FontSize(16);

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(50);   // N п/п
                            columns.RelativeColumn();     // Государства
                            columns.RelativeColumn();     // Столицы
                            columns.RelativeColumn();     // Население
                        });

                        // Заголовок
                        foreach (var cell in data[0])
                        {
                            table.Cell()
                                .Background(Colors.Grey.Lighten3)
                                .Padding(5)
                                .Text(cell).Bold();
                        }

                        // Данные
                        for (int row = 1; row < data.Length; row++)
                        {
                            foreach (var cell in data[row])
                            {
                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(5)
                                    .Text(cell);
                            }
                        }
                    });
                });
            })
            .GeneratePdf(fileName);

            // Правильное открытие PDF файла
            OpenPdfFile(fileName);

            static void OpenPdfFile(string fileName)
            {
                try
                {
                    // Способ 1: Используем ProcessStartInfo с UseShellExecute
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = fileName,
                        UseShellExecute = true
                    };
                    Process.Start(startInfo);

                    Console.WriteLine("PDF файл успешно создан и открыт!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Файл создан, но не удалось открыть автоматически: {ex.Message}");
                    Console.WriteLine($"Файл сохранен как: {Path.GetFullPath(fileName)}");
                }
            }
        }
    }
}