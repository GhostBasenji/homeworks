// Простой текстовый редактор

using System.Text;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Multiline = true; textBox1.Clear();
            this.Text = "Простой текстовый редактор";
            openFileDialog1.FileName = @"D:\Text2.txt";
            openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|All files (*.*)|*.*";
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Выводим диалог открытия файла
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == String.Empty) return;
            // Читаем текстовый файл:
            try
            {
                // Создаем экземпляр StreamReader для чтения из файла
                var Chitatel = new System.IO.StreamReader(
                    openFileDialog1.FileName);
                textBox1.Text = Chitatel.ReadToEnd();
                Chitatel.Close();
            }
            catch (System.IO.FileNotFoundException Situation)
            {
                MessageBox.Show(Situation.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Пункт меню "Сохранить как"
            saveFileDialog1.FileName = openFileDialog1.FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) Zapis();
        }
        private void Zapis()
        {
            try
            {
                // Создаем экземпляр StreamWriter для записи в файл
                var Pisatel = new System.IO.StreamWriter(
                    saveFileDialog1.FileName, false);
                    
                Pisatel.Write(textBox1.Text);
                Pisatel.Close(); textBox1.Modified = false;
            }
            catch (Exception Situation)
            {
                MessageBox.Show(Situation.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Обрабатываем момент закрытия формы:
            if (textBox1.Modified == false) return;
            // Если текст был изменен, то спрашиваем - сохранять ли изменения?
            var MBox = MessageBox.Show(
                "Текст был изменен. Сохранить изменения?",
                "Простой редактор", MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Exclamation);
            // YES — диалог:  NO — выход:  CANCEL — редактировать 
            if (MBox == DialogResult.No) return;
            if (MBox == DialogResult.Cancel) e.Cancel = true;
            if (MBox == DialogResult.Yes)
            {
                // Если да, то сохраняем текст в файл
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                { Zapis(); return; }
                else e.Cancel = true; // Передумал выходить из ПГМ
            } // DialogResult.Yes
        }
    }
}


// Как видно из кода, при обработке события загрузки формы присваиваем начальные значения некоторым переменным. В частности, для открытия (Open) и сохранения (Save)
// файлов заказываем фильтр (Filter) для текстовых файлов *.txt, а также для всех файлов *.*. 
// При обработке события "щелчок на пункте меню Открыть" выводим стандартное диалоговое окно открытия файлов OpenFileDialog, и если полученное в результате диалога 
// имя файла не пусто (Empty), то организуем чтение текстового файла.
// Аналогично написана обработка события "щелчок на пункте меню Сохранить как". Выводится стандартное диалоговое окно SaveFileDialog сохранения файла, и если пользователь
// щелкнул на кнопке OK (или Сохранить), то вызывается процедура Zapis(). Если нет, то пользователь отправляется редактировать текст.
// Как можно видеть, в процедуре Zapis() попытка (try) записи заканчивается оператором textBox1.Modified = false. Свойство Modified отслеживает изменения в текстовом поле.
// Понятно, что сразу после записи в файл следует это свойство перевести в состояние false. 
// Наибольший интерес представляет организация выхода из программы. Выйти из программы можно либо через пункт меню Выход, либо закрывая программу традиционными методами
// Windows, т. е. нажатием комбинации клавиш <Alt>+<F4>, кнопки Закрыть или кнопки системного меню (слева вверху) (обработка события закрытия формы FormClosing). Момент
// закрытия формы отслеживаем с помощью события формы FormClosing, которое происходит во время закрытия формы. Обратите внимание, выход по пункту меню Выход организован
// не через метод Application.Exit(), а через закрытие формы this.Close(). Почему? Потому что метод Application.Exit() не вызывает событие формы FormClosing.
// Обычно выход из любого редактора (текстового, графического, табличного и т. д.) реализуется по следующему алгоритму: если пользователь не сделал никаких изменений в 
// редактируемом файле, то надо выйти из программы. Изменения в текстовом поле регистрируются свойством textBox1.Modified. Если в документе имеются несохраненные изменения
// (textBox1.Modified = true), а пользователь хочет выйти из программы, то выводят диалоговое окно сохранения файла. 