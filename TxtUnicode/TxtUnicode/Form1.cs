// Программа для чтения/записи текстового файла в кодировке Unicode

using static System.Net.Mime.MediaTypeNames;

namespace TxtUnicode
{
    public partial class Form1 : Form
    {
        // Объявляем NameFile вне всех подпрограмм, чтобы эта переменная была "видна" во всех процедурах обработок событий:
        String NameFile;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Multiline = true; textBox1.Clear();
            button1.Text = "Открыть"; button1.TabIndex = 0;
            button2.Text = "Сохранить";
            this.Text = "Здесь кодировка Unicode";
            NameFile = @"D:\Text1.txt";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Русские буквы будут корректно читаться, если открыть файл в кодировке Unicode:
            try
            {
                // Создаем объект StreamReader для чтения файла:
                var Chitatel = new System.IO.StreamReader(NameFile);
                // Непосредствено читаем весь файл в текстовое поле:
                textBox1.Text = Chitatel.ReadToEnd();
                Chitatel.Close(); // Закрываем файл

                // Читать текстовый файл в кодировке Unicode в массив строк можно также таким образом (без Open и Close):
                // var MassivStrok = System.IO.File.ReadAllLines(@"D:\Text1.txt");
            }
            catch (System.IO.FileNotFoundException Situation)
            {
                // Обработка исключения, если файл не найден:
                MessageBox.Show(Situation.Message + "\n" +
                    "Нет такого файла", "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception Situation)
            {
                // Отчет о других ошибках:
                MessageBox.Show(Situation.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаем объект StreamWriter для записи в файл
                var Pisatel = new System.IO.StreamWriter(NameFile, false);
                Pisatel.Write(textBox1.Text);
                Pisatel.Close(); // Закрываем файл

                // Сохранить текстовый файл можно также таким образом (без Close), причем если файл уже существует, 
                // то он будет заменен: System.IO.File.WriteAllText(@"D:\tmp.tmp", textBox1.Text);
            }
            catch (Exception Situation)
            {
                // Отчет о всех возможных ошибках:
                MessageBox.Show(Situation.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}


// Несколько слов о блоках try, которые, как можно видеть, присутствуют в данном коде. Логика использования try следующая: попытаться(try) выполнить некоторую
// задачу, например прочитать файл. Если задача решена некорректно(например файл не найден), то "перехватить"(catch) управление и обработать возникшую
// (исключительную, Exception) ситуацию. Как видно из текста кода, обработка исключительной ситуации свелась к информированию пользователя о недоразумении.

// При обработке события "щелчок на кнопке Открыть" организован ввод файла D:\Text1.txt. Обычно в такой ситуации для выбора файла применяют элемент управления
// OpenFileDialog. Мы не стали использовать этот элемент управления для того, чтобы не "заговорить" проблему, а также свести к минимуму программный код.
// Далее создаем объект(поток) Chitatel для чтения из файла. Затем следует чтение файла NameFile методом ReadToEnd() в текстовое поле TextBox1.Text и закрытие файла методом Close(). 

// При обработке события "щелчок на кнопке Сохранить" аналогично организована запись файла на диск через объект Pisatel. При создании объекта Pisatel первым
// аргументом является NameFile, а второй аргумент false указывает, что данные следует не добавить(append) к содержимому файла(если он уже существует), а перезаписать
// (overwrite). Запись на диск производится с помощью метода Write() из свойства Text элемента управления TextBox1.