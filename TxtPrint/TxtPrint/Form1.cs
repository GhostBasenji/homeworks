// Программа позволяет открыть в стандартном диалоговом окне текстовый файл, 
// просмотреть его в текстовом поле без возможности изменения текста 
// (ReadOnly) и при желании пользователя вывести этот текст на принтер 

namespace TxtPrint
{
    public partial class Form1 : Form
    {
        System.IO.StreamReader Читатель;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Открытие текстового файла и его печать";
            textBox1.Multiline = true; textBox1.Clear();
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.ReadOnly = true;

            // До тех пор, пока файл не прочитан в текстовое поле, не должен быть виден пункт меню "Печать"
            печатьToolStripMenuItem.Visible = false;
            openFileDialog1.FileName = null;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == null) return;
            try
            {
                // Создаем поток StreamReader для чтения текстового файла
                Читатель = new System.IO.StreamReader(
                               openFileDialog1.FileName,
                               System.Text.Encoding.GetEncoding(1251));
                textBox1.Text = Читатель.ReadToEnd();
                Читатель.Close();
                печатьToolStripMenuItem.Visible = true;
            }
            catch (System.IO.FileNotFoundException Ситуация)
            {
                MessageBox.Show(Ситуация.Message + "\nНет такого файла", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception Ситуация)
            {
                MessageBox.Show(Ситуация.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Читатель = new System.IO.StreamReader(
                               openFileDialog1.FileName,
                               System.Text.Encoding.GetEncoding(1251));
                try
                {
                    printDocument1.Print();
                }
                finally
                {
                    Читатель.Close();
                }
            }
            catch (Exception Ситуация)
            {
                MessageBox.Show(Ситуация.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Событие вывода на печать страницы (PrintPage)
            // Чтобы получить пустой обработчик этого события, можно дважды щелкнуть на значке printDocument1 в дизайнере формы
            Single СтрокНаСтранице = 0.0F;
            Single Y = 0;
            var ЛевыйКрай = e.MarginBounds.Left;
            var ВерхнийКрай = e.MarginBounds.Top;
            var Строка = String.Empty;
            var Шрифт = new Font("Times New Roman", 12.0F);
            // Вычисляем количество строк на одной странице
            СтрокНаСтранице = e.MarginBounds.Height / Шрифт.GetHeight(e.Graphics);
            // Печатаем каждую строку текста
            var i = 0; // - счет строк
            while (i < СтрокНаСтранице)
            {
                Строка = Читатель.ReadLine();
                if (Строка == null) break;
                Y = ВерхнийКрай + i * Шрифт.GetHeight(e.Graphics);
                e.Graphics.DrawString(Строка, Шрифт, Brushes.Black, ЛевыйКрай, Y, new StringFormat());
                i = i + 1; // или i+=1; или i++;
                // Печать следующей страницы, если есть еще строки файла
                if (Строка != null) e.HasMorePages = true;
                else e.HasMorePages = false;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

// Здесь при обработке события загрузки формы Form1_Load запрещаем пользователю  редактировать текстовое поле: ReadOnly = true. Также назначаем свойству 
// ПечатьToolStripMenuItem.Visible = false (пункт меню Печать), т.е. в начале работы программы пункт меню Печать пользователю не виден (поскольку пока
// распечатывать нечего — необходимо вначале открыть текстовый файл). Остальные присваивания при обработке события Form1_Load очевидны.
// При обработке события "щелчок на пункте меню Открыть" вызываем стандартный диалог openFileDialog и организуем чтение файла через создание потока StreamReader. 
// После чтения файла в текстовое поле назначаем видимость пункту меню Печать: печатьToolStripMenuItem.Visible = true, поскольку уже есть, что печатать на принтере (файл открыт). 
// Представляет интерес обработка события "щелчок на пункте меню Печать". Здесь во вложенных блоках try...finally...catch программа еще раз создает поток StreamReader, а затем
// запускает процесс печати документа printDocument1.Print. Если ничего более не программировать, только метод printDocument1.Print, то принтер распечатает лишь пустую страницу. 
// Чтобы принтер распечатал текст, необходимо обработать событие PrintPage (см. текст программы), которое создает объект PrintDocument. То есть роль метода Print — это создать 
// событие PrintPage. 
// Обратите внимание на обработку события printDocument1_PrintPage. Вначале перечислены объявления переменных, значения некоторых из них получаем из аргументов события e, 
// например, ЛевыйКрай — значение отступа от левого края, и т. д. Назначаем шрифт печати — Times New Roman, 12 пунктов. 
// Далее в цикле while программа читает каждую строку Строка из файла —  Читатель.ReadLine(), а затем распечатывает ее командой (методом) DrawString. Здесь используется графический
// объект Graphics, который получаем из аргумента события e. 
// В переменной i происходит счет строк. Если количество строк оказывается большим, чем число строк на странице, то происходит выход из цикла, поскольку страница распечатана. Если 
// есть еще страницы, а программа выясняет это, анализируя содержимое переменной Строка, — если ее содержимое отличается от значения null (Строка != null), то аргументной переменной
// e.HasMorePages назначаем true, что инициирует опять событие PrintPage, и процедура printDocument1_PrintPage начинает свою работу вновь. И так, пока не закончатся все страницы
// e.HasMorePages = false для печати на принтере.