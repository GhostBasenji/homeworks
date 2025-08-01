// Данная программа "на лету" генерирует PDF-документ.
// Добавляем библиотеку QuestPDF через NuGet.

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

using System.Diagnostics;
using System.IO;

namespace CreatePdf1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Регистрируем
            QuestPDF.Settings.License = LicenseType.Community;  // Бесплатная лицензния MIT
            string pdfPath = "Отчет_QuestPDF.pdf";

            // Создаем PDF-документ
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4); // Формат А4
                    page.Margin(2, Unit.Centimetre);  // Отступы 2 см
                    page.DefaultTextStyle(x => x.FontSize(14));  // Размер шрифта

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(col =>
                        {
                            col.Item().Text("Hello!").Bold().FontSize(20);
                            col.Item().Text("This is a PDF created through QuestPDF.");
                            col.Item().Text($"Date: {DateTime.Now:dd.MM.yyyy}");
                        });
                });
            })
            .GeneratePdf(pdfPath);  // Сохраняем в файл

            // Открываем PDF (если возможно)
            if (File.Exists(pdfPath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка открытия PDF: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("PDF не создан!");
            }
        }
    }
}