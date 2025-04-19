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
