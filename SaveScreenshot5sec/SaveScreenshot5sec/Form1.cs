// Программа после запуска каждые пять секунд делает снимок текущего состояния экрана и записывает эти снимки в
// файлы Pic1.BMP, Pic2.BMP и т. д. Количество таких записей в файл — пять 

namespace SaveScreenshot5sec
{
    public partial class Form1 : Form
    {
        int i; // счет секунд

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            i = 0;
            this.Text = "Запись каждые 5 секунд в файл";
            button1.Text = "Пуск";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i = i + 1;
            this.Text = String.Format("Прошло {0} секунд", i);

            if (i >= 28) { timer1.Enabled = false; this.Close(); }
            Single Ostatok = i % 5;

            if (Ostatok != 0) return;

            // Имитируем нажатие клавиш <Alt>+<PrintScree>
            SendKeys.Send("%{PRTSC}");

            // Объявляем объект-получатель из буфера обмена
            var Recipient = Clipboard.GetDataObject();

            // Если данные в буфере обмена представлены в формате Bitmap, то записать
            if (Recipient.GetDataPresent(DataFormats.Bitmap) == true)
            {
                // эти данные из буфера обмена в переменную Rastr в формате Bitmap
                var Objekt = Recipient.GetData(DataFormats.Bitmap);
                var Rastr = (Bitmap)Objekt;

                // Сохраняем изображение из переменной Rastr в файл D:\Pic1, D:\Pic2, D:\Pic3, ...
                var ImyaFaila = String.Format(@"D:\Pic{0}.bmp", i / 5);
                Rastr.Save(ImyaFaila);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = String.Format("Прошло 0 секунд");
            timer1.Interval = 1000;  // равно 1 секунде
            timer1.Enabled = true; // время пошло
        }
    }
}

// Как видно из кода, структура программы включает в себя обработку события "щелчок на кнопке" button1_Click и обработку события timer1_Tick.
// В начале программы задаем переменную i, которая считает прошедшие секунды. При щелчке на кнопке Пуск задаем интервал времени Interval,
// равный 1000 миллисекунд, т. е. одной секунде. Далее даем команду таймеру начать отсчет времени timer1.Enabled = True, и через каждую секунду
// наступает событие timer1_Tick(), т. е. управление переходит этой процедуре.
// При обработке события timer1_Tick наращиваем значение переменной i, которая ведет счет секунд после старта таймера. Выражение i % 5 вычисляет
// целочисленный остаток после деления первого числового выражения на второе. Понятно, что если число i будет кратно пяти, то этот остаток будет
// равен нулю, и только в этом случае будет происходить имитация нажатия клавиш <Alt>+<PrintScreen> и запись содержимого буфера обмена в файл. 