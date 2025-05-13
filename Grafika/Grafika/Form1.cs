// Программа рисует график объемов продаж по месяцам. Понятно, что таким же образом можно построить любой график по точкам для других прикладных целей 

namespace Grafika
{
    public partial class Form1 : Form
    {
        String[] Months;
        int[] Sales;
        Graphics Grafika;

        // Далее создаем объект Bitmap, который имеет тот же размер и разрешение, что и PictureBox
        Bitmap Rastr;
        int OtstupLeft, OtstupRight, OtstupBottom, OtstupTop;
        int DlinaVertOsi, DlinaHorizOsi, YGorizontOsi, Xmax, XStartEpur;

        // Шаг градуировки по горизонтальной и вертикальной осям:
        Double GorizontStep;
        int VerticalStep;
        int i;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Исходные данные для построения графика (т.е. исходные точки):
            Months = new String[] { "Янв", "Фев", "Март", "Апр", "Май", "Июнь", "Июль", "Авг", "Сент", "Окт", "Нояб", "Дек" };
            Sales = new int[] { 335, 414, 572, 629, 750, 931, 753, 599, 422, 301, 245, 155 };
            OtstupLeft = 35; OtstupRight = 15; OtstupBottom = 20; OtstupTop = 10;
            this.Text = "Построение графика";
            button1.Text = "Нарисовать график";
            this.ClientSize = new Size(593, 319);
            pictureBox1.Size = new Size(569, 242);
            Rastr = new Bitmap(pictureBox1.Width, pictureBox1.Height, pictureBox1.CreateGraphics());

            // Нарисовать границу pictureBox1 для отладки:
            // pictureBox1.BorderStyle = BorderStyle.FixedSingle;

            YGorizontOsi = pictureBox1.Height - OtstupBottom;
            Xmax = pictureBox1.Width - OtstupRight;
            DlinaHorizOsi = pictureBox1.Width - (OtstupLeft + OtstupRight);
            DlinaVertOsi = YGorizontOsi - OtstupTop;
            GorizontStep = Convert.ToDouble(DlinaHorizOsi / Sales.Length);
            VerticalStep = Convert.ToInt32(DlinaVertOsi / 10);
            XStartEpur = OtstupLeft + 30;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Последовательно вызываем следующие процедуры:
            Grafika = Graphics.FromImage(Rastr);
            RisuemOsi();
            RisuemGorizontLinii();
            RisuemVertLinii();
            RisuemEpur();
            pictureBox1.Image = Rastr;
            // Освобождаем ресурсы, используемые объектом класса Graphics:
            Grafika.Dispose();
        }

        void RisuemOsi()
        {
            var Pero = new Pen(Color.Black, 2);
            
            // Рисуем вертикальную ось координат:
            Grafika.DrawLine(Pero, OtstupLeft, YGorizontOsi, OtstupLeft, OtstupTop);

            // Рисуем горизонтальную ось координат:
            Grafika.DrawLine(Pero, OtstupLeft, YGorizontOsi, Xmax, YGorizontOsi);

            var Shrift = new Font("Arial", 8);
            for (this.i = 1; i <= 10; i++)
            {
                // Рисуем "усики" на вертикальной координатной оси:
                int Y = YGorizontOsi - i * VerticalStep;
                Grafika.DrawLine(Pero, OtstupLeft - 4, Y, OtstupLeft, Y);

                // Подписываем значения продаж через каждые 100 единиц:
                Grafika.DrawString((i * 100).ToString(), Shrift, Brushes.Black, 0, Y - 5);
            }
            // Подписываем месяцы на горизонтальной оси:
            for (this.i = 0; i <= Months.Length - 1; i++)
            {
                Grafika.DrawString(Months[i], Shrift, Brushes.Black,
                    (int)(OtstupLeft + 18 + i * GorizontStep), (YGorizontOsi + 4));
            }
        }

        void RisuemGorizontLinii()
        {
            var TonkoePero = new Pen(Color.LightGray, 1);
            for (this.i = 1; i <= 10; i++)
            {
                // Рисуем горизонтальные почти "прозрачные" линии:
                int Y = YGorizontOsi - VerticalStep * i;
                Grafika.DrawLine(TonkoePero, OtstupLeft + 3, Y, Xmax, Y);
            }
            TonkoePero.Dispose();
        }

        void RisuemVertLinii()
        {
            // Рисуем вертикальные "почти" прозрачные линии
            var TonkoePero = new Pen(Color.Bisque, 1);
            for (this.i = 0; i <= Months.Length - 1; i++)
            {
                int X = XStartEpur + Convert.ToInt32(GorizontStep * i);
                Grafika.DrawLine(TonkoePero, X, OtstupTop, X, YGorizontOsi - 4);
            }
            TonkoePero.Dispose();
        }

        void RisuemEpur()
        {
            var VertMasshtab = Convert.ToDouble(DlinaVertOsi / 1000.0);
            // Или
            // var VertMasshtab = (Double)DlinaVertOsi / 1000.0;

            // Значения ординат на экране:
            var Y = new int[Sales.Length];

            // Значения абсцисс на экране:
            var X = new int[Sales.Length];

            for (this.i = 0; i <= Sales.Length - 1; i++)
            {
                // Вычисляем графические координаты точек:
                Y[i] = YGorizontOsi - Convert.ToInt32(GorizontStep * i); // Отнимаем значения продаж, поскольку ось Y экрана направлена вниз

                X[i] = XStartEpur + Convert.ToInt32(GorizontStep * i);
            }

            // Рисуем первый кружок:
            var Pero = new Pen(Color.Blue, 3);
            Grafika.DrawEllipse(Pero, X[0] - 2, Y[0] - 2, 4, 4);
            for (this.i = 0; i <= Sales.Length - 2; i++)
            {
                // Цикл по линиям между точками:
                Grafika.DrawLine(Pero, X[i], Y[i], X[i + 1], Y[i + 1]);

                // Отнимаем 2, поскольку диаметр (ширина) точки = 4:
                Grafika.DrawEllipse(Pero, X[i + 1] - 2, Y[i + 1] - 2, 4, 4);
            }
        }
    }
}


// Вначале объявляем некоторые переменные, чтобы они были видны из всех процедур класса. Строковый массив Months содержит названия месяцев, которые
// пользователь нашего программного кода может менять в зависимости от контекста строящегося графика. В любом случае записанные строки в этом массиве 
// будут отображаться по горизонтальной оси графика. Массив целых чисел Sales содержит объемы продаж по каждому месяцу, они соответствуют вертикальным
// ординатам графика. Оба массива должны иметь между собой одинаковую размерность, но не обязательно равную двенадцати. 
// При обработке события "щелчок мыши на кнопке Button" создаем объект класса Graphics, используя элемент управления PictureBox (графическое поле), а затем,
// вызывая соответствующие процедуры, поэтапно рисуем координатные оси, сетку из горизонтальных и вертикальных линий и непосредственно эпюру. 
// Чтобы успешно, минимальными усилиями, с возможностью дальнейшего совершенствования программы построить график, следует как можно более понятно назвать
// некоторые ключевые, часто встречающиеся интервалы и координаты на рисунке. Из названий этих интервалов будет следовать смысл. Скажем, переменная ОтступСлева
// хранит число пикселов, на которое следует отступать, чтобы строить на графике, например, вертикальную ось продаж. Кроме очевидных названий упомянем переменную
// YГоризОси — это графическая ордината (ось x направлена слева направо, а ось y — сверху вниз) горизонтальной оси графика, на которой подписываются месяцы.
// Переменная XMax содержит значение максимальной абсциссы, правее которой уже никаких построений нет. Переменная XНачЭпюры — это значение абсциссы первой
// построенной точки графика.