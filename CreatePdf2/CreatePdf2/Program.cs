// Данная программа "на лету" генерирует PDF-документ с текстом на русском языке
// Устанавливается библиотека QuestPDF через Nuget

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;

namespace CreatePdf2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Настройка лицензии (бесплатная версия)
            QuestPDF.Settings.License = LicenseType.Community;

            // Путь к файлу PDF
            string pdfPath = "Отчет.pdf";

            // Создание PDF
            QuestPDF.Fluent.Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            // Используем стандартные шрифты или подключаем свои
                            column.Item()
                                .Text(text =>
                                {
                                    text.Span("Здравствуйте!").FontFamily("Comic Sans MS");
                                    text.EmptyLine();
                                    text.Span("Вы увлекаетесь .NET 8?").FontFamily("Consolas");
                                });
                        });
                });
            })
            .GeneratePdf(pdfPath);

            // Открытие PDF (корректный способ)
            if (File.Exists(pdfPath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии PDF: {ex.Message}");
                }
            }
        }
    }
}