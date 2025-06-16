// Программа формирует таблицу на основании двух массивов переменных с двойной точностью. Данную таблицу программа демонстрирует 
// пользователю в текстовом поле TextBox. Есть возможность распечатать таблицу на принтере 

using System.Linq.Expressions;

namespace TablTxtPrint
{
    public partial class Form1 : Form
    {
        System.IO.StringReader Chitatel; // - внешняя переменная
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Escape-последовательность для перехода на новую строку:
            const String HC = "\r\n";   // Новая строка
            this.Text = "Формирование таблицы";
            Double[] X = {
                5342736.17653, 2345.3333, 234683.853749,
                2438454.825368, 3425.72564, 5243.25,
                537407.6236, 6354328.9876, 5342.243};
            Double[] Y = {
                27488.17, 3806703.356, 22345.72,
                54285.34, 2236767.3267, 57038.76,
                201722.3, 26434.001, 2164.022};

            textBox1.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Font = new Font("Courier New", 9.0F);
            textBox1.Text = "КАТАЛОГ КООРДИНАТ" + HC;
            textBox1.Text += "---------------------------------" + HC;
            textBox1.Text += "|Пункт|      X     |     Y      |" + HC;
            textBox1.Text += "---------------------------------" + HC;

            for (int i = 0; i <= 8; i++)
                textBox1.Text += String.Format("| {0,3:D} | {1,10:F2} | {2,10:F2} |", i, X[i], Y[i]) + HC;
            textBox1.Text += "---------------------------------" + HC;

            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 0;
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаем поток Chitatel для чтения из строки:
                Chitatel = new System.IO.StringReader(textBox1.Text);
                try
                {
                    printDocument1.Print();
                }
                finally
                {
                    Chitatel.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


// Как видно из кода, формирование таблицы также происходит в цикле for с помощью функции String.Format.В фигурных скобках числа 0, 1 и 2 означают,
// что вместо фигурных скобок следует вставлять переменные i, X[i], Y[i].Выражение "3:D" означает, что переменную i следует размещать в трех символах
// по формату целых переменных "D".Выражение "10:F2" означает, что переменную X[i] следует размещать в десяти символах по фиксированному формату с
// двумя знаками после запятой.
// При обработке события "щелчок на пункте меню Печать" в блоках try...finaly...catch создаем поток Читатель, однако не для чтения из файла, а для чтения
// из текстовой переменной textBox1.Text.В этом случае мы обращаемся с потоком Читатель так же, как при операциях с файлами, но совершенно не обращаясь к
// внешней памяти(диску). Поэтому организация многостраничной печати остается абсолютно такой же, как в ДЗ "TxtPrint".
// Как видно из программы, для того чтобы просмотреть, откорректировать и распечатать на принтере таблицу(инженерных или экономических вычислений), совершенно
// необязательно записывать эту таблицу в текстовый файл и читать его Блокнотом.