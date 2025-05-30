// Программа для чтения/записи бинарных файлов с использованием потока данных

namespace ReadWriteBin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Успеваемость студента";
            label1.Text = "Номер п/п"; label2.Text = "Фамилия И.О.";
            label3.Text = "Средний балл";
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear();
            button1.Text = "Читать"; button2.Text = "Сохранить";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ЧТЕНИЕ БИНАРНОГО ФАЙЛА 
            // Если такого файла нет
            if (System.IO.File.Exists(@"D:\student.usp") == false) return;

            // Создаем поток для чтения
            var Читатель = new System.IO.BinaryReader(
                               System.IO.File.OpenRead(@"D:\student.usp"));
            try
            {
                var Номер_пп = Читатель.ReadInt32();
                var ФИО = Читатель.ReadString();
                var Средний_балл = Читатель.ReadSingle();
                textBox1.Text = Convert.ToString(Номер_пп);
                textBox2.Text = Convert.ToString(ФИО);
                textBox3.Text = Convert.ToString(Средний_балл);
            }
            finally { Читатель.Close(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // ЗАПИСЬ БИНАРНОГО ФАЙЛА
            // Создаем поток для записи байтов в файл
            var Писатель = new System.IO.BinaryWriter(
                               System.IO.File.Open(@"D:\student.usp",
                               System.IO.FileMode.Create));
            try
            {
                var Номер_пп = Convert.ToInt32(textBox1.Text);
                var ФИО = Convert.ToString(textBox2.Text);

                // Разрешаем в качестве разделителя целой и дробной части как запятую, так и точку:
                textBox3.Text = textBox3.Text.Replace('.', ',');
                var Средний_балл = Convert.ToSingle(textBox3.Text);
                Писатель.Write(Номер_пп);
                Писатель.Write(ФИО);
                Писатель.Write(Средний_балл);
            }
            finally { Писатель.Close(); }
        }
    }
}


// Как видим, в процедуре обработки события загрузки формы организована инициализация(присвоение начальных значений) элементов формы: текстовых полей, меток и кнопок.
// Запись файла на диск происходит при обработке события button2.Click, т. е. щелчка мышью на кнопке Сохранить. С этой целью создаем поток байтов Писатель для открытия
// файла student.usp. Если такого файла не существует, то он создается (Create), а если файл уже есть, то он перезаписывается. Как видно из текста программы, для ее
// упрощения мы не использовали элемент управления OpenFileDialog открытия файла в диалоге.
// Далее преобразуем записанное в текстовых полях в более естественные типы данных. Номер по порядку Номер_пп — это тип целых чисел, преобразование в целый тип может быть
// реализовано операцией Convert.ToInt32 (можно использовать другие функции преобразования), для переменной Средний_балл (средний балл) больше всего подходит тип с плавающей
// точкой Single, при этом преобразование осуществляется операцией Convert.ToSingle. Преобразование для строковой переменной ФИО является необязательным и приведено для
// симметрии записей. Операторы Писатель.Write записывают эти данные в файл. После блока finally происходит обязательное закрытие (Close) файла. 
// Чтение файла выполняется при обработке события "щелчок мышью на кнопке "Читать". Как уже упоминалось, для максимального упрощения в данной программе не предусмотрено
// открытие файла через стандартное диалоговое окно, поэтому в начале процедуры выясняем, существует ли такой файл. Если файла D:\student.usp нет, то программируем выход
// (return) из обработчика данного события. Заметьте, чтобы программисту было максимально легко отслеживать ветви оператора if, мы написали: "Если файла нет, то return".
// При этом длинная ветвь логики "если файл есть" не включена непосредственно в оператор if. Поэтому такой фрагмент программного кода читается (воспринимается) программистом
// легко — это типичный пример хорошо написанного программного кода.
// Далее создается поток байтов Читатель из файла student.usp, открытого для чтения. Чтение из потока в каждую переменную реализовано с помощью функции ReadInt32 — читать
// из потока Читатель в переменную целого типа, аналогично функциям ReadString и ReadSingle. Затем осуществлено конвертирование этих переменных в строковый тип Convert.ToString.
// Как видно из программы, можно было изначально все текстовые поля записывать в файл без конвертирования, но при дальнейшем развитии этой программы значения полей все равно
// пришлось бы преобразовывать в соответствующий тип. После блока finally происходит закрытие (Close) файла. Блок finally выполнится всегда, даже если перед ним была команда return. 
// Дальнейшее развитие этой программы может быть осуществлено путем добавления в файл сведений о других студентах. В таком случае при чтении файла количество студентов будет
// неизвестно. Тогда следует обработать ситуацию достижения конца файла: catch (EndOfStreamException e) а затем закрыть файл. 
